using CullingSystem;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using OldSchoolGraphics.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

namespace OldSchoolGraphics.Comps;
internal sealed class FogPrelit : MonoBehaviour
{
    public static FogPrelit Current;

    private Il2CppStructArray<Vector4> _Arr_Vec1;
    private Il2CppStructArray<Vector4> _Arr_Vec2;
    private Il2CppStructArray<Matrix4x4> _Arr_Matrix;
    private Il2CppStructArray<float> _Arr_Float1;

    private Color _PrelitColor;
    private float _PrelitRange;

    public void Setup()
    {
        _Arr_Vec1 = new Il2CppStructArray<Vector4>(32);
        _Arr_Vec2 = new Il2CppStructArray<Vector4>(32);
        _Arr_Matrix = new Il2CppStructArray<Matrix4x4>(32);
        _Arr_Float1 = new Il2CppStructArray<float>(32);
    }

    void Start()
    {
        Current = this;
    }

    void OnDestroy()
    {
        Current = null;
    }

    public void UpdateClusteredEntry(ClusteredRendering cr, CommandBuffer cmd)
    {
        var prelit = PreLitVolume.Current;

        for (var i = 0; i < _Arr_Vec1.Length; i++)
        {
            var col = ((Color)cr.m_effectColorPhysical[i]).linear;
            var heighest = Mathf.Max(col.r, col.g, col.b);
            if (heighest > CFG.FogLit.PointColorMaxCap)
            {
                col = (col / heighest) * CFG.FogLit.PointColorMaxCap;
            }
            
            col = col.RGBMultiplied(CFG.FogLit.EffectColorScale);
            col.a = 1.0f;
            _Arr_Vec1[i] = col;
        }
        cmd.SetGlobalVectorArray(ClusteredRendering._EffectColorPhysical, _Arr_Vec1);
    }

    public void RevertClusteredEntry(ClusteredRendering cr, CommandBuffer cmd)
    {
        cmd.SetGlobalVectorArray(ClusteredRendering._EffectColorPhysical, cr.m_effectColorPhysical);
        //cmd.SetGlobalVectorArray(ClusteredRendering._EffectPosInvRangeSq, cr.m_effectPosInvRangeSq);
        //cmd.SetGlobalFloatArray(ClusteredRendering._EffectParams, cr.m_effectParams);


        //cmd.SetGlobalVectorArray(ClusteredRendering._PointColorPhysical, cr.m_pointColorPhysical);
        //cmd.SetGlobalVectorArray(ClusteredRendering._SpotColor, cr.m_spotColor);

        //cmd.SetComputeIntParam(ClusteredRendering.s_compute, ClusteredRendering._EffectCount, 1);
    }

    void Update()
    {
        foreach (var spot in C_CullingManager.VisibleSpotLights)
        {
            var cl = spot.m_clusterLight;
            if (cl != null)
            {
                cl.UpdateData();
            }
        }

        foreach (var point in C_CullingManager.VisiblePointLights)
        {
            var cl = point.m_clusterLight;
            if (cl != null)
            {
                cl.UpdateData();
            }
        }

        //var prelit = PreLitVolume.Current;
        //if (prelit != null)
        //{
        //    _PrelitColor = prelit.m_fogColor.linear * CFG.FogLit.PreLitIntensity;
        //    _PrelitColor.a = 1.0f;

        //    prelit.DensityBoost = CFG.FogLit.FogBoost;
        //}

        //_PrelitRange = prelit.m_fogDistance * 0.5f;
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //private void InjectFogPreLit(ClusteredRendering cr, CommandBuffer cmd)
    //{
    //    var prelit = PreLitVolume.Current;
    //    _Arr_Vec1[0] = _PrelitColor;
    //    cmd.SetGlobalVectorArray(ClusteredRendering._EffectColorPhysical, _Arr_Vec1);

    //    var pos = transform.position;
    //    _Arr_Vec1[0] = new Vector4(pos.x, pos.y, pos.z, 1.0f / (_PrelitRange * _PrelitRange));
    //    cmd.SetGlobalVectorArray(ClusteredRendering._EffectPosInvRangeSq, _Arr_Vec1);

    //    _Arr_Float1[0] = 0.0f;
    //    cmd.SetGlobalFloatArray(ClusteredRendering._EffectParams, _Arr_Float1);

    //    _Arr_Vec1[0] = new Vector4(pos.x, pos.y, pos.z, 10000.0f);
    //    cmd.SetComputeVectorArrayParam(ClusteredRendering.s_compute, ClusteredRendering._EffectCullData, _Arr_Vec1);
    //    cmd.SetComputeIntParam(ClusteredRendering.s_compute, ClusteredRendering._EffectCount, 1);
    //}
}
