using GTFO.API;
using Il2CppInterop.Runtime;
using OldSchoolGraphics.Configurations;
using OldSchoolGraphics.Controllers.ShaderInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OldSchoolGraphics.Controllers;
internal static class EmissionUpdater
{
    private static readonly Dictionary<int, EmissionInfo> _Lookup = new();
    private const float BASE_MULT = 1.625f;

    private static readonly EmissiveShaderInfo[] _ESInfos = new EmissiveShaderInfo[]
    {
        new ESI_Standard(),
        new ESI_CMSWorldProp(),
        new ESI_DisplayHologram(),
        new ESI_DisplayGearShader(),
        new ESI_GearShader(),
        new ESI_CustomGearShader()
    };

    private static readonly Dictionary<string, float> _MaterialIntensityOverrides = new()
    {
        { "ApexLights", 1.05f },
        { "terminal_display_screen", 1.22f },
        { "MWP", 1.05f },
        { "prop_Datacenter_Databall_1", 1.15f },
        { "DefoggerBig", 1.05f }
    };

    public static void Init()
    {
        EmissiveShaderInfo.Initialize();
        foreach (var esi in _ESInfos)
        {
            esi.Setup();
        }

        LevelAPI.OnBuildDone += OnBuildDone;
        LevelAPI.OnLevelCleanup += OnLevelCleanup;
    }

    private static void OnLevelCleanup()
    {
        foreach (var info in _Lookup.Values)
        {
            info.Reset();
        }
        _Lookup.Clear();
    }

    private static void OnBuildDone()
    {
        System.Diagnostics.Stopwatch sw = new();

        sw.Restart();
        var renderers = UnityEngine.Object.FindObjectsOfTypeIncludingAssets(Il2CppType.Of<Renderer>());
        Add(renderers.Select(x => x.Cast<Renderer>().sharedMaterial).ToArray());
        sw.Stop();
        Logger.Error($"Time Elapsed: {sw.ElapsedTicks} ({sw.ElapsedMilliseconds}ms)");
        Logger.Error($"Items: Valid items: {_Lookup.Count} / out of {renderers.Count}");
    }

    public static void Add(Material[] materials)
    {
        int length = materials.Length;
        for (int i = 0; i<length; i++)
        {
            Add(materials[i]);
        }
    }

    public static void Add(Material material)
    {
        if (material == null)
            return;

        var shader = material.shader;
        if (shader == null)
            return;

        if (_Lookup.ContainsKey(material.GetInstanceID()))
            return;

        if (Evaluate(material, out var info))
        {
            info.BaseMultiplier = BASE_MULT;
            if (_MaterialIntensityOverrides.TryGetValue(material.name, out var mult))
            {
                info.BaseMultiplier = mult;
            }
            info.UpdateEmission(CFG.Emission.ObjectBloomScale);
            _Lookup[material.GetInstanceID()] = info;
        }
    }

    public static void UpdateAllEmission(float multiplier)
    {
        foreach (var info in _Lookup.Values)
        {
            info.UpdateEmission(multiplier);
        }
    }

    private static bool Evaluate(Material material, out EmissionInfo info)
    {
        int length = _ESInfos.Length;
        for (var i = 0; i < length; i++)
        {
            var esi = _ESInfos[i];
            if (esi.IsValid(material))
            {
                info = esi.CreateInfo(material);
                return info != null;
            }
        }

        info = null;
        return false;
    }
}

internal class EmissionInfo
{
    public Material SharedMaterial;
    public Color OriginalColor;
    public int PropertyID;
    public float BaseMultiplier;

    public static EmissionInfo Create(Material material, int colorProperty)
    {
        return new EmissionInfo()
        {
            SharedMaterial = material,
            PropertyID = colorProperty,
            OriginalColor = material.GetColor(colorProperty)
        };
    }

    public void UpdateEmission(float multiplier)
    {
        SharedMaterial.SetColor(PropertyID, OriginalColor * multiplier * BaseMultiplier);
    }

    public void Reset()
    {
        SharedMaterial.SetColor(PropertyID, OriginalColor);
    }
}