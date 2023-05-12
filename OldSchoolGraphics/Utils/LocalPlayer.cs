using GTFO.API.Utilities;
using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldSchoolGraphics.Utils;
internal static class LocalPlayer
{
    private static PlayerAgent _LocalPlayerAgent;
    private static FPSCamera _FPSCam;

    internal static void Init()
    {
        CoroutineDispatcher.StartCoroutine(Update());
    }

    private static IEnumerator Update()
    {
        while(true)
        {
            _LocalPlayerAgent = PlayerManager.GetLocalPlayerAgent();
            _FPSCam = _LocalPlayerAgent != null ? _LocalPlayerAgent.FPSCamera : null;
            yield return null;
        }
    }

    public static bool TryGet(out PlayerAgent localPlayer)
    {
        localPlayer = _LocalPlayerAgent;
        return localPlayer != null;
    }

    public static bool TryGetFPSCam(out FPSCamera fpsCam)
    {
        fpsCam = _FPSCam;
        return fpsCam != null;
    }
}
