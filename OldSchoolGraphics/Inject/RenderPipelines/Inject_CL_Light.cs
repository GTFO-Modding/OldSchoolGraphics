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
        var light = __instance.UnityLight;
        var transform = __instance.transform;

        RetoneColor(CFG.FogLit.SpotColorMaxCap, data.Position, ref col, out var rangeBoost);
        data.Irradiance = col * CFG.FogLit.SpotColorScale;

        var newVirtualRange = light.range + rangeBoost;
        data.InvRangeSqr *= 1.0f / (newVirtualRange * newVirtualRange);

        var pos = transform.position;
        var rot = transform.rotation;
        var scale = 2.0f * newVirtualRange * Vector3.one;
        __instance.LightMatrix = Matrix4x4.TRS(pos, rot, scale);
        __instance.Data = data;
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(CL_PointLight), nameof(CL_PointLight.UpdateData))]
    static void Post_UpdatePoint(CL_PointLight __instance)
    {
        var data = __instance.Data;
        var col = data.Irradiance;
        var light = __instance.UnityLight;
        var transform = __instance.transform;

        RetoneColor(CFG.FogLit.PointColorMaxCap, data.Position, ref col, out var rangeBoost);
        data.Irradiance = col * CFG.FogLit.PointColorScale;

        var newVirtualRange = light.range + rangeBoost;
        data.InvRangeSqr *= 1.0f / (newVirtualRange * newVirtualRange);

        var pos = transform.position;
        var rot = transform.rotation;
        var factor = newVirtualRange * Mathf.Tan(light.spotAngle * 0.5f * 0.0174532924f);
        var scale = new Vector3(factor, factor, newVirtualRange);
        __instance.LightMatrix = Matrix4x4.TRS(pos, rot, scale);
        __instance.Data = data;
    }

    static void RetoneColor(float maxCap, Vector3 pos, ref Vector3 lit, out float rangeBoost)
    {
        var brightness = lit.x * 0.212f + lit.y * 0.701f + lit.z * 0.087f;
        var reMaxCap = maxCap + GetDensityLitBonus(pos);
        var diff = brightness - reMaxCap;
        if (diff > 0.0f)
        {
            lit = (lit / brightness) * reMaxCap;
            rangeBoost = diff * CFG.FogLit.LitAdjustment_IntensityToRangeWeight;
        }
        else
        {
            rangeBoost = 0.0f;
        }
    }

    static float GetDensityLitBonus(Vector3 pos)
    {
        var prelit = PreLitVolume.Current;
        if (prelit == null)
        {
            return 0.0f;
        }

        var density = prelit.GetFogDensity(pos);
        var p = Mathf.InverseLerp(CFG.FogLit.LitAdjustment_MinDensity, CFG.FogLit.LitAdjustment_MaxDensity, density);
        return p * CFG.FogLit.LitAdjustment_DensityBonusAmount;
    }
}
