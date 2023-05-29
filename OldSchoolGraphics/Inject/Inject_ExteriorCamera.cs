using ExteriorRendering;
using OldSchoolGraphics.Controllers;
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
    [HarmonyPatch(nameof(ExteriorCamera.SetEnabled))]
    static void Post_OnEnable(ExteriorCamera __instance)
    {
        OldSchoolSettings.ApplyPPSettings(__instance.m_fpsCamera);
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(ExteriorCamera.SetDisabled))]
    static void Post_OnDisable(ExteriorCamera __instance)
    {
        OldSchoolSettings.ApplyPPSettings(__instance.m_fpsCamera);
    }
}
