using BepInEx;
using BepInEx.Configuration;
using GTFO.API.Utilities;
using OldSchoolGraphics.Controllers;
using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldSchoolGraphics;
internal static class CFG
{
    public static ConfigEntry<float> BloomScale { get; private set; }
    public static ConfigEntry<float> ObjectBloomScale { get; private set; }
    public static ConfigEntry<float> ExposureScale { get; private set; }
    public static ConfigEntry<float> NoiseScale { get; private set; }
    public static ConfigEntry<float> ContrastFactor { get; private set; }

    public static ConfigEntry<float> EnemyGlowScale { get; private set; }
    public static ConfigEntry<MeleeOverride> MeleeType { get; private set; }
    public static ConfigEntry<float> FlashlightSwayFactor { get; private set; }
    public static ConfigEntry<float> DustParticleFactor { get; private set; }

    public static float DEBUG_Value1 { get; private set; } = 1.0f;
    public static float DEBUG_Value2 { get; private set; } = 1.0f;
    public static float DEBUG_Value3 { get; private set; } = 1.0f;

#if DEBUG
    private static ConfigEntry<float> _DEBUG_Value1;
    private static ConfigEntry<float> _DEBUG_Value2;
    private static ConfigEntry<float> _DEBUG_Value3;
#endif

    private static ConfigFile _Config;

    private const string GRAPHIC = "Graphics";
    private const string USER_PREF = "User Preferences";
    private const string DEV = "Debug";

    internal static void Initialize(ConfigFile cfg)
    {
        _Config = cfg;

        BloomScale = _Config.Bind(GRAPHIC, "Bloom Scale", 1.0f);
        ExposureScale = _Config.Bind(GRAPHIC, "Exposure Scale", 1.0f);
        NoiseScale = _Config.Bind(GRAPHIC, "Noise Scale", 1.0f);
        ContrastFactor = _Config.Bind(GRAPHIC, "Contrast Factor", 1.0f);

        MeleeType = _Config.Bind(USER_PREF, "Hammer Model", MeleeOverride.Default);
        ObjectBloomScale = _Config.Bind(USER_PREF, "Object Bloom Scale", 1.0f);
        EnemyGlowScale = _Config.Bind(USER_PREF, "Sleeper Glow Scale", 1.0f);
        FlashlightSwayFactor = _Config.Bind(USER_PREF, "Flashlight Sway Movement", 0.35f, "(0.0 - 1.0) Lower = less movement from center");
        DustParticleFactor = _Config.Bind(USER_PREF, "Dust Particle Factor", 0.7f, "Aka Dust Particle Size");

#if DEBUG
        _DEBUG_Value1 = _Config.Bind(DEV, "Value1", 1.0f);
        _DEBUG_Value2 = _Config.Bind(DEV, "Value2", 1.0f);
        _DEBUG_Value3 = _Config.Bind(DEV, "Value3", 1.0f);
#endif

        var listener = LiveEdit.CreateListener(Paths.ConfigPath, "OldSchoolGraphics.cfg", false);
        listener.FileChangedEventCooldown = 1.0f;
        listener.FileChanged += CFG_FileChanged;
    }

    private static void CFG_FileChanged(LiveEditEventArgs e)
    {
        CoroutineDispatcher.StartCoroutine(ReloadConfig());
    }

    private static IEnumerator ReloadConfig()
    {
        while (true)
        {
            try
            {
                _Config.Reload();
#if DEBUG
                DEBUG_Value1 = _DEBUG_Value1.Value;
                DEBUG_Value2 = _DEBUG_Value2.Value;
                DEBUG_Value3 = _DEBUG_Value3.Value;
#endif
                if (PlayerManager.HasLocalPlayerAgent())
                {
                    var player = PlayerManager.GetLocalPlayerAgent();
                    if (player.FPSCamera != null)
                    {
                        OldSchoolSettings.ApplyPPSettings(player.FPSCamera);
                    }
                }
                EmissionUpdater.UpdateAllEmission(ObjectBloomScale.Value);
                break;
            }
            catch
            {
                
            }
            yield return null;
        }
    }
}

internal enum MeleeOverride
{
    Default,
    Gavel,
    Maul,
    Sledgehammer,
    Mallet
}
