using GameData;
using Gear;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldSchoolGraphics.Inject.MeleeParts;
[HarmonyPatch(typeof(GearPartSpawner), nameof(GearPartSpawner.AddPart))]
internal class Inject_GearPartSpawner
{
    static void Prefix(ref GearPartGeneralData general)
    {
        switch (CFG.MeleeType.Value)
        {
            case MeleeOverride.Gavel:
                HammerInfo.Default.ReplaceTo(HammerInfo.Gavel, ref general);
                break;

            case MeleeOverride.Maul:
                HammerInfo.Default.ReplaceTo(HammerInfo.Maul, ref general);
                break;

            case MeleeOverride.Sledgehammer:
                HammerInfo.Default.ReplaceTo(HammerInfo.Sledge, ref general);
                break;

            case MeleeOverride.Mallet:
                HammerInfo.Default.ReplaceTo(HammerInfo.Mallet, ref general);
                break;

            default:
                return;
        }
    }
}

internal class HammerInfo
{
    public string HeadPrefab;
    public string NeckPrefab;
    public string HandlePrefab;
    public string PommelPrefab;

    private GearPartGeneralData _HeadData = null;
    public GearPartGeneralData HeadData
    {
        get
        {
            if (_HeadData != null)
                return _HeadData;

            return _HeadData = new GearPartGeneralData()
            {
                AssetBundle = AssetBundleName.Gear_Melee_Head,
                GearCategoryFilter = 0,
                Model = HeadPrefab,
            };
        }
    }

    private GearPartGeneralData _NeckData = null;
    public GearPartGeneralData NeckData
    {
        get
        {
            if (_NeckData != null)
                return _NeckData;

            return _NeckData = new GearPartGeneralData()
            {
                AssetBundle = AssetBundleName.Gear_Melee_Neck,
                GearCategoryFilter = 0,
                Model = NeckPrefab,
            };
        }
    }

    private GearPartGeneralData _HandleData = null;
    public GearPartGeneralData HandleData
    {
        get
        {
            if (_HandleData != null)
                return _HandleData;

            return _HandleData = new GearPartGeneralData()
            {
                AssetBundle = AssetBundleName.Gear_Melee_Handle,
                GearCategoryFilter = 0,
                Model = HandlePrefab,
            };
        }
    }

    private GearPartGeneralData _PommelData = null;
    public GearPartGeneralData PommelData
    {
        get
        {
            if (_PommelData != null)
                return _PommelData;

            return _PommelData = new GearPartGeneralData()
            {
                AssetBundle = AssetBundleName.Gear_Melee_Pommel,
                GearCategoryFilter = 0,
                Model = PommelPrefab,
            };
        }
    }

    public void ReplaceTo(HammerInfo replaceTo, ref GearPartGeneralData general)
    {
        if (HeadPrefab.Equals(general.Model, StringComparison.InvariantCultureIgnoreCase))
        {
            general = replaceTo.HeadData;
        }
        else if (NeckPrefab.Equals(general.Model, StringComparison.InvariantCultureIgnoreCase))
        {
            general = replaceTo.NeckData;
        }
        else if (HandlePrefab.Equals(general.Model, StringComparison.InvariantCultureIgnoreCase))
        {
            general = replaceTo.HandleData;
        }
        else if (PommelPrefab.Equals(general.Model, StringComparison.InvariantCultureIgnoreCase))
        {
            general = replaceTo.PommelData;
        }
    }

    public static HammerInfo Gavel = new()
    {
        HeadPrefab = "Assets/AssetPrefabs/Items/Gear/Parts/Melee/Heads/Head_Hammer_10.prefab",
        NeckPrefab = "Assets/AssetPrefabs/Items/Gear/Parts/Melee/Necks/Neck_Hammer_5.prefab",
        HandlePrefab = "Assets/AssetPrefabs/Items/Gear/Parts/Melee/Handles/Handle_Hammer_7.prefab",
        PommelPrefab = "Assets/AssetPrefabs/Items/Gear/Parts/Melee/Pommels/Pommel_Hammer_10.prefab",
    };

    public static HammerInfo Maul = new()
    {
        HeadPrefab = "Assets/AssetPrefabs/Items/Gear/Parts/Melee/Heads/Head_Hammer_7.prefab",
        NeckPrefab = "Assets/AssetPrefabs/Items/Gear/Parts/Melee/Necks/Neck_Hammer_8.prefab",
        HandlePrefab = "Assets/AssetPrefabs/Items/Gear/Parts/Melee/Handles/Handle_Hammer_1.prefab",
        PommelPrefab = "Assets/AssetPrefabs/Items/Gear/Parts/Melee/Pommels/Pommel_Hammer_5.prefab"
    };

    public static HammerInfo Sledge = new()
    {
        HeadPrefab = "Assets/AssetPrefabs/Items/Gear/Parts/Melee/Heads/Head_Hammer_1.prefab",
        NeckPrefab = "Assets/AssetPrefabs/Items/Gear/Parts/Melee/Necks/Neck_Hammer_4.prefab",
        HandlePrefab = "Assets/AssetPrefabs/Items/Gear/Parts/Melee/Handles/Handle_Hammer_8.prefab",
        PommelPrefab = "Assets/AssetPrefabs/Items/Gear/Parts/Melee/Pommels/Pommel_Hammer_3.prefab"
    };

    public static HammerInfo Mallet = new()
    {
        HeadPrefab = "Assets/AssetPrefabs/Items/Gear/Parts/Melee/Heads/Head_Hammer_6.prefab",
        NeckPrefab = "Assets/AssetPrefabs/Items/Gear/Parts/Melee/Necks/Neck_Hammer_7.prefab",
        HandlePrefab = "Assets/AssetPrefabs/Items/Gear/Parts/Melee/Handles/Handle_Hammer_9.prefab",
        PommelPrefab = "Assets/AssetPrefabs/Items/Gear/Parts/Melee/Pommels/Pommel_Hammer_9.prefab"
    };

    public static HammerInfo Default = new()
    {
        HeadPrefab = "Assets/AssetPrefabs/Items/Gear/Parts/Melee/Heads/Head_Hammer_5.prefab",
        NeckPrefab = "Assets/AssetPrefabs/Items/Gear/Parts/Melee/Necks/Neck_Hammer_10.prefab",
        HandlePrefab = "Assets/AssetPrefabs/Items/Gear/Parts/Melee/Handles/Handle_Hammer_6.prefab",
        PommelPrefab = "Assets/AssetPrefabs/Items/Gear/Parts/Melee/Pommels/Pommel_Hammer_10.prefab"
    };
}
