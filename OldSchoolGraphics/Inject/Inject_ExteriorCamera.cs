using ExteriorRendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldSchoolGraphics.Inject;
[HarmonyPatch(typeof(ExteriorCamera))]
internal class Inject_ExteriorCamera
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(ExteriorCamera.OnEnable))]
    static void Post_OnEnable(ExteriorCamera __instance)
    {
        OldSchoolSettings.Apply(__instance.m_fpsCamera);
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(ExteriorCamera.OnDisable))]
    static void Post_OnDisable(ExteriorCamera __instance)
    {
        OldSchoolSettings.Apply(__instance.m_fpsCamera);
    }
}
