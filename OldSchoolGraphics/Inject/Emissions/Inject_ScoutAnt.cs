﻿using Enemies;
using OldSchoolGraphics.Configurations;
using OldSchoolGraphics.Controllers;
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
        __instance.m_colorDefault *= OldSchoolSettings.EMISSION_MULT * 1.35f * CFG.Emission.ScoutAntGlowScale;
        __instance.m_colorDetection *= OldSchoolSettings.EMISSION_MULT * 1.35f * CFG.Emission.ScoutAntGlowScale;
    }
}
