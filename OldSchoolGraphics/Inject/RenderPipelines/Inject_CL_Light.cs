using OldSchoolGraphics.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OldSchoolGraphics.Inject.RenderPipelines;
[HarmonyPatch]
internal class Inject_CL_Light
{
    [HarmonyPostfix]
    [HarmonyPatch(typeof(CL_SpotLight), nameof(CL_SpotLight.UpdateData))]
    static void Post_UpdateSpot(CL_SpotLight __instance)
    {
        if (__instance.UnityLight.GetInstanceID() == LocalPlayer.FPSSpotInstanceID)
        {
            return;
        }

        var data = __instance.Data;
        var col = data.Irradiance;
        var heighest = Mathf.Max(col.x, col.y, col.z);
        var maxCap = CFG.FogLit.SpotColorMaxCap * DensityMult(data.Position);
        var diff = heighest - CFG.FogLit.SpotColorMaxCap;
        if (diff > 0)
        {
            col = (col / heighest) * maxCap;
        }

        data.Irradiance = col * CFG.FogLit.SpotColorScale;
        data.InvRangeSqr *= 1.0f / (diff + 1.25f);
        __instance.Data = data;
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(CL_PointLight), nameof(CL_PointLight.UpdateData))]
    static void Post_UpdatePoint(CL_PointLight __instance)
    {
        var data = __instance.Data;
        var col = data.Irradiance;
        var heighest = Mathf.Max(col.x, col.y, col.z);
        var maxCap = CFG.FogLit.PointColorMaxCap * DensityMult(data.Position);
        var diff = heighest - maxCap;
        if (diff > 0)
        {
            col = (col / heighest) * maxCap;
        }

        data.Irradiance = col * CFG.FogLit.PointColorScale;
        data.InvRangeSqr *= 1.0f / (diff + 1.25f);
        __instance.Data = data;
    }

    static float DensityMult(Vector3 pos)
    {
        var prelit = PreLitVolume.Current;
        if (prelit == null)
        {
            return 1.0f;
        }

        return Mathf.InverseLerp(0.05f, 0.0f, prelit.GetFogDensity(pos)) + 0.35f;
    }
}
