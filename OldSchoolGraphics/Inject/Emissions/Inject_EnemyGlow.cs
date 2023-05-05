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
        col *= (4.25f * CFG.EnemyGlowScale.Value);
    }

    [HarmonyPrefix]
    [HarmonyPatch(nameof(EnemyAppearance.InterpolateGlow))]
    [HarmonyPatch(new Type[] { typeof(Color), typeof(float) })]
    [HarmonyPriority(Priority.Last)]
    static void Pre_InterpolateGlow2(ref Color col)
    {
        col *= (4.25f * CFG.EnemyGlowScale.Value);
    }
}
