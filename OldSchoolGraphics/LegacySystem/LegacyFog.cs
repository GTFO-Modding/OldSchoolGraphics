using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

namespace OldSchoolGraphics.LegacySystem;
internal class LegacyFog
{
    public static RenderTexture VolumeRT { get; private set; }

    internal static void Init()
    {
        
    }

    private static void GenerateVolumeRT()
    {
        if (VolumeRT != null)
        {
            UnityEngine.Object.Destroy(VolumeRT);
            VolumeRT = null;
        }

        var baseGameRT = PreLitVolume.Current.m_fogVolume;
        var newRT = new RenderTexture(Screen.width / 8, Screen.height / 8, baseGameRT.depth)
        {
            dimension = TextureDimension.Tex3D,
            volumeDepth = baseGameRT.volumeDepth,
            enableRandomWrite = true,
            useMipMap = false
        };
        newRT.Create();
        newRT.hideFlags = HideFlags.HideAndDontSave;
    }

    public static void SetEnabled(bool enabled)
    {
        if (enabled)
        {

        }
    }
}
