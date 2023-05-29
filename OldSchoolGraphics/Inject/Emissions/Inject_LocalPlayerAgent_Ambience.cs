using OldSchoolGraphics.Configurations;
using OldSchoolGraphics.Controllers;
using Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldSchoolGraphics.Inject.Emissions;
[HarmonyPatch(typeof(LocalPlayerAgent), nameof(LocalPlayerAgent.Setup))]
internal class Inject_LocalPlayerAgent_Ambience
{
    static void Postfix(LocalPlayerAgent __instance)
    {
        __instance.m_ambientLightColor = ColorExt.Hex("#6ee7ff") * 1.0f;
        if (__instance.m_ambientLight != null)
        {
            __instance.m_ambientLight.Range *= 0.99f;
            __instance.m_ambientLight.Intensity *= 2.02f * OldSchoolSettings.EMISSION_MULT * CFG.Emission.PlayerAmbientIntensity;
        }
    }
}
