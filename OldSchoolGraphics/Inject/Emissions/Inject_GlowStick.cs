using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldSchoolGraphics.Inject.Emissions;
[HarmonyPatch(typeof(GlowstickInstance), nameof(GlowstickInstance.Setup))]
internal class Inject_GlowStick
{
    static void Postfix(GlowstickInstance __instance)
    {
        __instance.m_LightColorTarget *= 1.4f;
    }
}
