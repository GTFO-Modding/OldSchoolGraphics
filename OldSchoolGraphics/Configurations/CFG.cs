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

namespace OldSchoolGraphics.Configurations;
internal static class CFG
{
    public static CFG_Graphics Graphic { get; private set; } = new();
    public static CFG_Emissions Emission { get; private set; } = new();
    public static CFG_User User { get; private set; } = new();
    public static CFG_Debug DEBUG { get; private set; } = new();

    private static ConfigFile _Config;

    internal static void Initialize(string configFileName)
    {
        var configFilePath = Path.Combine(Paths.ConfigPath, configFileName);
        _Config = new ConfigFile(configFilePath, saveOnInit: true);

        Graphic.Initialize(_Config);
        Emission.Initialize(_Config);
        User.Initialize(_Config);
        DEBUG.Initialize(_Config);

        var listener = LiveEdit.CreateListener(Paths.ConfigPath, configFileName, includeSubDir: false);
        listener.FileChangedEventCooldown = 0.75f;
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
                Reloaded();
                break;
            }
            catch
            {

            }
            yield return null;
        }
    }

    private static void Reloaded()
    {
        DEBUG.UpdateValue();
        if (LocalPlayer.TryGetFPSCam(out var fpsCam))
        {
            OldSchoolSettings.ApplyPPSettings(fpsCam);
            Logger.Warn("Reloading Config");
        }
        EmissionUpdater.UpdateAllEmission(Emission.ObjectBloomScale);
        Logger.Warn("Adjusting Object Bloom");
    }
}
