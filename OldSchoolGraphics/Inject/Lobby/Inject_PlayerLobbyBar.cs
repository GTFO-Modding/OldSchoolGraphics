using CellMenu;
using GTFO.API.Utilities;
using OldSchoolGraphics.Comps;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OldSchoolGraphics.Inject.Lobby;
[HarmonyPatch(typeof(CM_PlayerLobbyBar))]
internal class Inject_PlayerLobbyBar
{
    //TODO: Add Gray PlayerLobbyBar Charactor
    //[HarmonyPatch(nameof(CM_PlayerLobbyBar.SpawnPlayerModel))]
    //[HarmonyPostfix]
    static void Post_PlayerModelSpawned(CM_PlayerLobbyBar __instance)
    {
        ScreenBlackAndWhite.Instance.AddTrackingTransform(__instance.m_playerModelAlign);
        ScreenBlackAndWhite.Instance.UpdateCommand();
    }

    //[HarmonyPatch(nameof(CM_PlayerLobbyBar.StoreBackpackVanityItems))]
    //[HarmonyPostfix]
    static void Post_VanityItemChanged()
    {
        ScreenBlackAndWhite.Instance.UpdateCommand();
    }
}
