global using HarmonyLib;
global using OldSchoolGraphics.Utils;
global using Il2CppInterop.Runtime.Attributes;

using AssetShards;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Unity.IL2CPP;
using GTFO.API;
using Il2CppInterop.Runtime.Injection;
using OldSchoolGraphics.Comps;
using OldSchoolGraphics.Controllers;
using System;
using System.IO;
using System.Linq;
using UnityEngine;
using OldSchoolGraphics.LegacySystem;
using OldSchoolGraphics.Configurations;

namespace OldSchoolGraphics;
[BepInPlugin("OldSchoolGraphics.GUID", "OldSchoolGraphics", VersionInfo.Version)]
[BepInDependency("dev.gtfomodding.gtfo-api", BepInDependency.DependencyFlags.HardDependency)]
internal class EntryPoint : BasePlugin
{
    private Harmony _Harmony = null;

    public override void Load()
    {
        CFG.Initialize("OldSchoolGraphics.cfg");

        ClassInjector.RegisterTypeInIl2Cpp<ScreenGrains>();
        ClassInjector.RegisterTypeInIl2Cpp<ScreenBlackAndWhite>();
        ClassInjector.RegisterTypeInIl2Cpp<Comps.DebugBehaviour>();

        _Harmony = new Harmony($"{VersionInfo.RootNamespace}.Harmony");
        _Harmony.PatchAll();
        Logger.Info($"Plugin has loaded with {_Harmony.GetPatchedMethods().Count()} patches!");
        AssetAPI.OnStartupAssetsLoaded += AssetAPI_OnStartupAssetsLoaded;
    }

    private void AssetAPI_OnStartupAssetsLoaded()
    {
        foreach (var shard in Enum.GetValues<AssetBundleShard>())
        {
            AssetShardManager.LoadShard(AssetShardManager.GetShardName(AssetBundleName.Gear_Melee_Head, shard));
            AssetShardManager.LoadShard(AssetShardManager.GetShardName(AssetBundleName.Gear_Melee_Neck, shard));
            AssetShardManager.LoadShard(AssetShardManager.GetShardName(AssetBundleName.Gear_Melee_Handle, shard));
            AssetShardManager.LoadShard(AssetShardManager.GetShardName(AssetBundleName.Gear_Melee_Pommel, shard));
        }
        EmissionUpdater.Init();
        LocalPlayer.Init();
        CustomAssets.Init();
        LegacyFog.Init();

#if DEBUG
        var mgr = new GameObject("mgr");
        GameObject.DontDestroyOnLoad(mgr);
        mgr.AddComponent<Comps.DebugBehaviour>();
#endif
    }

    public override bool Unload()
    {
        _Harmony.UnpatchSelf();
        return base.Unload();
    }
}