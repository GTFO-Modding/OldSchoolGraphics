using GameData;
using Gear;
using OldSchoolGraphics.Configurations;
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

    [HarmonyPatch(nameof(PlayerInventoryBase.PrepareHelmetFlashlight))]
    [HarmonyPostfix]
    static void Post_HelmetLight(PlayerInventoryBase __instance)
    {
        __instance.m_flashlight.intensity *= CFG.Emission.FlashlightIntenisty * 1.5f;
    }

    [HarmonyPatch(nameof(PlayerInventoryBase.OnItemEquippableFlashlightWielded))]
    [HarmonyPostfix]
    static void Post_GearLight(PlayerInventoryBase __instance, GearPartFlashlight flashlight)
    {
        if (flashlight.m_settingsID > 0)
        {
            var data = FlashlightSettingsDataBlock.GetBlock(flashlight.m_settingsID);
            __instance.m_flashlight.intensity = data.intensity * CFG.Emission.FlashlightIntenisty * 1.5f;
        }
    }
}
