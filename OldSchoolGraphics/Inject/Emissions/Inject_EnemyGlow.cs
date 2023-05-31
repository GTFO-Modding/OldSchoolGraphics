using OldSchoolGraphics.Configurations;
using OldSchoolGraphics.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OldSchoolGraphics.Inject.Emissions;
[HarmonyPatch(typeof(EnemyAppearance))]
internal class Inject_EnemyGlow
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(EnemyAppearance.InterpolateGlow))]
    [HarmonyPatch(new Type[] { typeof (Color), typeof(Vector4), typeof(float) })]
    [HarmonyPriority(Priority.Last)]
    static void Pre_InterpolateGlow1(ref Color col)
    {
        col *= (OldSchoolSettings.EMISSION_MULT * 3.78f * CFG.Emission.EnemyGlowScale);

        var brightness = col.r * 0.212f + col.g * 0.701f + col.b * 0.087f;

        var cap = CFG.Emission.EnemyGlowCap;
        if (brightness > cap)
        {
            var delta = brightness - cap;
            col = (col / brightness) * cap;
        }
    }

    [HarmonyPrefix]
    [HarmonyPatch(nameof(EnemyAppearance.InterpolateGlow))]
    [HarmonyPatch(new Type[] { typeof(Color), typeof(float) })]
    [HarmonyPriority(Priority.Last)]
    static void Pre_InterpolateGlow2(ref Color col)
    {
        col *= (OldSchoolSettings.EMISSION_MULT * 3.78f * CFG.Emission.EnemyGlowScale);

        var brightness = col.r * 0.212f + col.g * 0.701f + col.b * 0.087f;

        var cap = CFG.Emission.EnemyGlowCap;
        if (brightness > cap)
        {
            var delta = brightness - cap;
            col = (col / brightness) * cap;
        }
    }
}
