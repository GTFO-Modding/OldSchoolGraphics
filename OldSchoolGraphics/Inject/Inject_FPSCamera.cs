using BepInEx.Unity.IL2CPP.Utils;
using GTFO.API.Utilities;
using Il2CppInterop.Runtime;
using OldSchoolGraphics.Comps;
using OldSchoolGraphics.Controllers;
using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityStandardAssets.ImageEffects;

namespace OldSchoolGraphics.Inject;
[HarmonyPatch(typeof(FPSCamera))]
internal class Inject_FPSCamera
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(FPSCamera.Setup), new Type[] { typeof(LocalPlayerAgent) })]
    static void Pre_Setup(FPSCamera __instance)
    {
        if (__instance.m_isSetup)
            return;

        Logger.DebugOnly("Setup");
        OldSchoolSettings.AddGraphicComponents(__instance);
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(FPSCamera.RefreshPostEffectsEnabled))]
    static void Post_Refresh(FPSCamera __instance)
    {
        Logger.DebugOnly("Refreshed");
        OldSchoolSettings.ApplyPPSettings(__instance);
    }
}
