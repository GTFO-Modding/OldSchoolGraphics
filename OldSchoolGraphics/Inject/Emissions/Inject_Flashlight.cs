using GameData;
using Gear;
using OldSchoolGraphics.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldSchoolGraphics.Inject.Emissions;
[HarmonyPatch(typeof(PlayerInventoryBase))]
internal class Inject_Flashlight
{
    static float Mult => (1.15f / Math.Max(0.2f, CFG.ExposureScale.Value)) * OldSchoolSettings.EMISSION_MULT * 1.17f;

    [HarmonyPatch(nameof(PlayerInventoryBase.PrepareHelmetFlashlight))]
    [HarmonyPostfix]
    static void Post_HelmetLight(PlayerInventoryBase __instance)
    {
        __instance.m_flashlight.intensity *= Mult * 1.05f;
    }

    [HarmonyPatch(nameof(PlayerInventoryBase.OnItemEquippableFlashlightWielded))]
    [HarmonyPostfix]
    static void Post_GearLight(PlayerInventoryBase __instance, GearPartFlashlight flashlight)
    {
        if (flashlight.m_settingsID > 0)
        {
            var data = FlashlightSettingsDataBlock.GetBlock(flashlight.m_settingsID);
            __instance.m_flashlight.intensity = data.intensity * Mult;
        }
    }
}
