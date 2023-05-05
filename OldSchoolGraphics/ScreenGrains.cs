using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

namespace OldSchoolGraphics;
internal class ScreenGrains : MonoBehaviour
{
    private NoiseAndGrain _Noise;

    public void Setup(Camera targetCamera)
    {
        _Noise = targetCamera.gameObject.AddComponent<NoiseAndGrain>();
        _Noise.noiseShader = Shader.Find("Hidden/NoiseAndGrain");
        _Noise.dx11NoiseShader = Shader.Find("Hidden/NoiseAndGrainDX11");
        _Noise.noiseTexture = GetTexture();
        _Noise.tiling = Vector3.one * 128.0f;
        _Noise.blackIntensity = 0.15f;
        _Noise.whiteIntensity = 0.15f;
        _Noise.generalIntensity = 0.15f;
    }

    void Update()
    {
        _Noise.intensityMultiplier = CFG.NoiseScale.Value;
        _Noise.noiseTexture = GetTexture();
    }

    int index = 0;
    Texture2D GetTexture()
    {
        if (index > PE_BlueNoise.cNoiseLayers - 2)
        {
            index = 0;
        }
        return PE_BlueNoise.Singleton.m_noiseTextures[index++];
    }
}
