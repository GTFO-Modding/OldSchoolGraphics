using OldSchoolGraphics.Comps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OldSchoolGraphics.Inject;
[HarmonyPatch(typeof(PlayerInventoryBase), nameof(PlayerInventoryBase.UpdateFPSFlashlightAlignment))]
internal class Inject_PlayerFlashlight
{
    static void Postfix(PlayerInventoryBase __instance)
    {
        if (__instance.Owner == null)
            return;

        if (!__instance.Owner.IsLocallyOwned)
            return;

        if (__instance.Owner.Owner == null || __instance.Owner.Owner.IsBot)
            return;

        if (__instance.Owner.FPSCamera == null)
            return;

        if (__instance.m_flashlightCLight == null)
            return;

        var fpsCamera = __instance.Owner.FPSCamera;
        var camera = fpsCamera.m_camera;
        var light = __instance.m_flashlightCLight.m_unityLight;

        var lightTarget = camera.transform.position + (camera.transform.forward * 6.0f);
        var lightTargetDir = (lightTarget - light.transform.position).normalized;
        light.transform.rotation = Quaternion.LookRotation(Vector3.Lerp(lightTargetDir, light.transform.forward, CFG.FlashlightSwayFactor.Value));
    }
}
