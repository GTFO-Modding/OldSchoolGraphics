using Il2CppInterop.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityStandardAssets.ImageEffects;

namespace OldSchoolGraphics.Inject;
[HarmonyPatch(typeof(FPSCamera))]
internal class Inject_FPSCamera
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(FPSCamera.Awake))]
    static void Post_Awake(FPSCamera __instance)
    {
        __instance.m_camera.gameObject.AddComponent<ScreenGrains>().Setup(__instance.m_camera);
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(FPSCamera.RefreshPostEffectsEnabled))]
    static void Post_Refresh(FPSCamera __instance)
    {
        OldSchoolSettings.ApplyPPSettings(__instance);
        __instance.m_camera.gameObject.AddComponent<Grayscale>();
    }
}
