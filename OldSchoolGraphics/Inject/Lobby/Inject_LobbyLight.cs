using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldSchoolGraphics.Inject.Lobby;
//[HarmonyPatch(typeof(CM_PageLoadoutLighting), nameof(CM_PageLoadoutLighting.LateUpdate))]
internal class Inject_LobbyLight
{
    static void Postfix(CM_PageLoadoutLighting __instance)
    {
        __instance.RimLight.enabled = false;
        __instance.FillLight.enabled = false;
        __instance.MainLight.enabled = true;
        __instance.MainLight.intensity = 1.7f;
        __instance.FillLight.intensity = 0.75f;
        __instance.MainLight.color = ColorExt.Hex("#3f3f3f");
        __instance.RimLight.intensity = 2.0f;
        __instance.HelmetLightIntensityScale = 0.0f;
        __instance.AmbientIntensity = 10.0f;
        __instance.ReflectionIntensity = 0.0f;
        __instance.SmoothnessLimiter = 0.1f;
        __instance.RayScale = 0.01f;
        __instance.Bias = 1.0f;
    }
}
