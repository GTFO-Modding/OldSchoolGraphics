using Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldSchoolGraphics.Inject.Emissions;
[HarmonyPatch(typeof(ScoutAntenna), nameof(ScoutAntenna.Init))]
internal class Inject_ScoutAnt
{
    static void Postfix(ScoutAntenna __instance)
    {
        __instance.m_colorDefault *= 2.8f;
        __instance.m_colorDetection *= 2.8f;
    }
}
