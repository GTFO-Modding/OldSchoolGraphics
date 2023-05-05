global using HarmonyLib;
using AssetShards;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Unity.IL2CPP;
using GTFO.API;
using System;
using System.IO;
using System.Linq;

namespace OldSchoolGraphics;
[BepInPlugin("OldSchoolGraphics.GUID", "OldSchoolGraphics", VersionInfo.Version)]
[BepInDependency("dev.gtfomodding.gtfo-api", BepInDependency.DependencyFlags.HardDependency)]
internal class EntryPoint : BasePlugin
{
    private Harmony _Harmony = null;

    public override void Load()
    {
        CFG.Initialize(new ConfigFile(Path.Combine(Paths.ConfigPath, "OldSchoolGraphics.cfg"), true));

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
    }

    public override bool Unload()
    {
        _Harmony.UnpatchSelf();
        return base.Unload();
    }
}