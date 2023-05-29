using GTFO.API;
using Il2CppInterop.Runtime;
using OldSchoolGraphics.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

namespace OldSchoolGraphics.Comps;
internal class DebugBehaviour : MonoBehaviour
{
    public static DebugBehaviour Current;

    public int RTWidth;
    public int RTHeight;
    public RenderTexture Buffer1;
    public RenderTexture Buffer2;
    public RenderTexture Buffer3;

    public ComputeShader CS;
    public int Kernel;

    void Start()
    {
        Current = this;
        Buffer1 = CreateTexture(Screen.width / 8, Screen.height / 8);
        Buffer2 = CreateTexture(Screen.width / 8, Screen.height / 8);
        Buffer3 = CreateTexture(Screen.width / 8, Screen.height / 8);

        CS = AssetAPI.GetLoadedAsset<ComputeShader>("Assets/Modding/OSG/FogDebugger.compute");
        Kernel = CS.FindKernel("CSMain");

    }

    RenderTexture CreateTexture(int width, int height)
    {
        var rt = new RenderTexture(width, height, 0);
        rt.dimension = UnityEngine.Rendering.TextureDimension.Tex2D;
        rt.format = RenderTextureFormat.ARGBFloat;
        rt.enableRandomWrite = true;
        rt.useMipMap = false;
        rt.filterMode = FilterMode.Trilinear;
        rt.Create();

        rt.hideFlags = HideFlags.HideAndDontSave;
        return rt;
    }

    void AllocTexture(int width, int height)
    {
        if (Buffer1 != null) Destroy(Buffer1);
        if (Buffer2 != null) Destroy(Buffer2);
        if (Buffer3 != null) Destroy(Buffer3);

        Buffer1 = CreateTexture(width, height);
        Buffer2 = CreateTexture(width, height);
        Buffer3 = CreateTexture(width, height);

        CS.SetTexture(Kernel, "_Tex1", Buffer1);
        CS.SetTexture(Kernel, "_Tex2", Buffer2);
        CS.SetTexture(Kernel, "_Tex3", Buffer3);

        RTWidth = width;
        RTHeight = height;

        Logger.Error($"Textured Allocated: {width}x{height}");
    }

    void Update()
    {
        if (PreLitVolume.Current != null)
        {
            var prelit = PreLitVolume.Current;
            if (prelit.m_fogVolume.width != RTWidth || prelit.m_fogVolume.height != RTHeight)
            {
                AllocTexture(prelit.m_fogVolume.width, prelit.m_fogVolume.height);
            }

            CS.SetTexture(Kernel, "_FogVolume", PreLitVolume.Current.m_fogVolume);
            //CS.Dispatch(Kernel, RTWidth, RTHeight, 4);
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {

        }
    }

    public static void Dispatch(CommandBuffer cmd, Texture tex, int x, int y, int z)
    {
        cmd.SetComputeTextureParam(Current.CS, Current.Kernel, "_FogVolume", tex);
        cmd.DispatchCompute(Current.CS, Current.Kernel, x, y, z);
    }
}
