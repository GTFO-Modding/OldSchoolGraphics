using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

namespace OldSchoolGraphics.Comps;
internal class ScreenGrains : MonoBehaviour
{
    private NoiseAndGrain _Noise;
    private int _NoiseTextureIndex = 0;

    [HideFromIl2Cpp]
    public void Setup()
    {
        var targetCamera = gameObject.GetComponent<Camera>();
        _Noise = targetCamera.gameObject.AddComponent<NoiseAndGrain>();
        _Noise.noiseShader = Shader.Find("Hidden/NoiseAndGrain");
        _Noise.dx11NoiseShader = Shader.Find("Hidden/NoiseAndGrainDX11");
        _Noise.noiseTexture = GetTexture();
        _Noise.tiling = Vector3.one * 128.0f;
        _Noise.blackIntensity = 0.41f;
        _Noise.whiteIntensity = 0.61f;
        _Noise.generalIntensity = 0.4f;
        _Noise.monochrome = true;
    }

    [HideFromIl2Cpp]
    public void UpdateIntensity(float intensity)
    {
        _Noise.intensityMultiplier = intensity;
    }

    void Update()
    {
        _Noise.noiseTexture = GetTexture();
    }

    [HideFromIl2Cpp]
    Texture2D GetTexture()
    {
        if (_NoiseTextureIndex > PE_BlueNoise.cNoiseLayers - 2)
        {
            _NoiseTextureIndex = 0;
        }
        return PE_BlueNoise.Singleton.m_noiseTextures[_NoiseTextureIndex++];
    }
}
