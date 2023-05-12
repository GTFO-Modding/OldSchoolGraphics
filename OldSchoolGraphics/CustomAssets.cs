using GTFO.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OldSchoolGraphics;
internal static class CustomAssets
{
    public static GameObject LegacyParticlePrefab { get; private set; }

    public static void Init()
    {
        LegacyParticlePrefab = AssetAPI.GetLoadedAsset<GameObject>("Assets/OSG/DustParticle.prefab");
    }
}
