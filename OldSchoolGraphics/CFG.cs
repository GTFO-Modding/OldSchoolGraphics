using BepInEx;
using BepInEx.Configuration;
using GTFO.API.Utilities;
using Player;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldSchoolGraphics;
internal static class CFG
{
    public static ConfigEntry<MeleeOverride> MeleeType { get; private set; }
    public static ConfigEntry<float> BloomScale { get; private set; }
    public static ConfigEntry<float> ExposureScale { get; private set; }
    public static ConfigEntry<float> NoiseScale { get; private set; }
    public static ConfigEntry<float> EnemyGlowScale { get; private set; }

    private static ConfigFile _Config;
    private static string _ConfigPath;

    private const string GRAPHIC = "Graphics";
    private const string USER_PREF = "User Preferences";

    internal static void Initialize(ConfigFile cfg)
    {
        _Config = cfg;
        _ConfigPath = Path.Combine(Paths.ConfigPath, "OldSchoolGraphics.cfg");

        BloomScale = _Config.Bind(GRAPHIC, "Bloom Scale", 1.0f);
        ExposureScale = _Config.Bind(GRAPHIC, "Exposure Scale", 1.0f);
        NoiseScale = _Config.Bind(GRAPHIC, "Noise Scale", 1.0f);
        MeleeType = _Config.Bind(USER_PREF, "Hammer Model", MeleeOverride.Default);
        EnemyGlowScale = _Config.Bind(USER_PREF, "Sleeper Glow Scale", 1.0f);

        var listener = LiveEdit.CreateListener(Paths.ConfigPath, "OldSchoolGraphics.cfg", false);
        listener.FileChangedEventCooldown = 1.0f;
        listener.FileChanged += CFG_FileChanged;
    }

    private static void CFG_FileChanged(LiveEditEventArgs e)
    {
        _Config.Reload();

        if (PlayerManager.HasLocalPlayerAgent())
        {
            var player = PlayerManager.GetLocalPlayerAgent();
            if (player.FPSCamera != null)
            {
                OldSchoolSettings.ApplyPPSettings(player.FPSCamera);
            }
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
