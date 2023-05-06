using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OldSchoolGraphics.Controllers.ShaderInfos;
internal abstract class EmissiveShaderInfo
{
    protected static int Prop_EmissiveColor { get; private set; }
    protected static int Prop_EmissionColor { get; private set; }
    protected static int Prop_EnableEmissive { get; private set; }

    public static void Initialize()
    {
        Prop_EmissiveColor = Shader.PropertyToID("_EmissiveColor");
        Prop_EmissionColor = Shader.PropertyToID("_EmissionColor");
        Prop_EnableEmissive = Shader.PropertyToID("_EnableEmissive");
    }

    protected Shader ShaderAsset { get; private set; }
    protected abstract string ShaderAssetPath { get; }
    protected int ShaderInstanceID { get; private set; }
    public EmissiveShaderInfo Setup()
    {
        ShaderAsset = Shader.Find(ShaderAssetPath);
        ShaderInstanceID = ShaderAsset.GetInstanceID();
        return this;
    }

    public bool IsValid(Material material)
    {
        if (material == null)
            return false;

        var shader = material.shader;
        if (shader == null)
            return false;

        if (shader.GetInstanceID() != ShaderInstanceID)
            return false;

        return IsEmissiveMaterial(material);
    }

    public EmissionInfo CreateInfo(Material material)
    {
        var hasResult = TryCreateInfo(material, out var info);
        if (hasResult) return info;
        else return null;
    }

    protected virtual bool IsEmissiveMaterial(Material material)
    {
        return true;
    }
    protected abstract bool TryCreateInfo(Material material, out EmissionInfo info);
}
