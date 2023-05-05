﻿using Il2CppInterop.Runtime;
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
    [HarmonyPatch(nameof(FPSCamera.RefreshPostEffectsEnabled))]
    static void Post_Refresh(FPSCamera __instance)
    {
        OldSchoolSettings.Apply(__instance);
        __instance.m_camera.gameObject.AddComponent<Grayscale>();
    }
}
