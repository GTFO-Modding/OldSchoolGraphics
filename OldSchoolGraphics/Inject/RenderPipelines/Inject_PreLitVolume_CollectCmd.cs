using OldSchoolGraphics.Comps;
using OldSchoolGraphics.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

namespace OldSchoolGraphics.Inject.RenderPipelines;
[HarmonyPatch(typeof(PreLitVolume), nameof(PreLitVolume.CollectCommands))]
internal static class Inject_PreLitVolume_CollectCmd
{
    static ComputeBuffer _LightCluster__Back;
    public static ComputeBuffer NewFogLightCluster
    {
        get
        {
            if (_LightCluster__Back == null)
            {
                _LightCluster__Back = new ComputeBuffer(ClusteredRendering.Current.m_lightCluster.count, 80);
            }
            return _LightCluster__Back;
        }
    }

    static ComputeBuffer _EmptyBuffer__Back;
    static ComputeBuffer EmptyBuffer
    {
        get
        {
            if (_EmptyBuffer__Back == null)
            {
                _EmptyBuffer__Back = new ComputeBuffer(1, 80);
            }
            return _EmptyBuffer__Back;
        }
    }

    static void Prefix(CommandBuffer cmd)
    {
        FogPrelit.Current.UpdateClusteredEntry(ClusteredRendering.Current, cmd);
    }

    static void Postfix(CommandBuffer cmd)
    {
        FogPrelit.Current.RevertClusteredEntry(ClusteredRendering.Current, cmd);
        //Shader.SetGlobalVector(PreLitVolume._FogColor, PreLitVolume.Current.m_fogColor * 5.0f);
    }
}
