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
        __instance.m_ambientLightColor = ColorExt.Hex("#111146") * 1.35f;
        if (__instance.m_ambientLight != null)
        {
            __instance.m_ambientLight.Range *= 1.085f;
            __instance.m_ambientLight.Intensity *= 1.7f * OldSchoolSettings.EMISSION_MULT;
        }
    }
}
