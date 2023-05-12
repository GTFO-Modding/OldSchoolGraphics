﻿using GTFO.API;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.InteropTypes;
using OldSchoolGraphics.Comps;
using OldSchoolGraphics.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityStandardAssets.ImageEffects;
using Bloom = UnityEngine.Rendering.PostProcessing.Bloom;

namespace OldSchoolGraphics.Controllers;
internal static class OldSchoolSettings
{
    public const float EMISSION_MULT = 1.65f;

    private static ScreenGrains _ScreenGrains;

    public static void AddGraphicComponents(FPSCamera fpsCam)
    {
        _ScreenGrains = fpsCam.gameObject.AddComponent<ScreenGrains>();
        _ScreenGrains.Setup();
    }

    public static void ApplyPPSettings(FPSCamera fpsCam)
    {
        UpdateGlobalShader();
        UpdateGraphicComponents(fpsCam);
        UpdatePostProcessing(fpsCam);
    }

    private static void UpdateGraphicComponents(FPSCamera fpsCam)
    {
        if (_ScreenGrains != null)
        {
            _ScreenGrains.UpdateIntensity(CFG.Graphic.NoiseScale);
        }

        if (fpsCam.PrelitVolume != null)
        {
            var lit = fpsCam.PrelitVolume;
            lit.lightFogTonemap = 0.0f;
            lit.fogLightAttenuation = 100.0f;

            var rTex = Shader.GetGlobalTexture(PreLitVolume._FogVolume).Cast<RenderTexture>();
            var newRT = new RenderTexture(rTex.width, rTex.height, rTex.depth)
            {
                dimension = rTex.dimension,
                volumeDepth = rTex.volumeDepth,
                enableRandomWrite = true,
                useMipMap = false,
                wrapMode = rTex.wrapMode
            };
            newRT.Create();
            var compute = AssetAPI.GetLoadedAsset<ComputeShader>("Assets/OSG/LegacyFog.compute");
            var kernel = compute.FindKernel("CSMain");
            compute.SetVector("_Color", Color.white * CFG.DEBUG.V1);
            compute.SetTexture(kernel, "_FogVolume", newRT);
            compute.Dispatch(kernel, 8, 8, 8);
            Shader.SetGlobalTexture(PreLitVolume._FogVolume, newRT);
        }

        if (fpsCam.AmbientParticles != null)
        {
            var particle = fpsCam.AmbientParticles;
            var mat = particle.material;
            var factor = CFG.Graphic.DustScale;
            mat.SetFloat("_SizeMin", 0.002f * factor);
            mat.SetFloat("_SizeMax", 0.003f * factor);
        }
    }

    private static void UpdateGlobalShader()
    {
        Shader.SetGlobalFloat("_SampleNoiseAmount", CFG.Graphic.NoiseScale);
    }

    private static void UpdatePostProcessing(FPSCamera fpsCam)
    {
        fpsCam.SetBloomEnabled(true);

        var beProfile = fpsCam.postProcessing.m_ppBehavior.profile;
        beProfile.bloom.enabled = true;

        var volProfile = fpsCam.postProcessing.m_ppVolume.profile;

        foreach (var setting in volProfile.settings)
        {
            if (TryCast(setting, out AutoExposure exposure))
            {
                exposure.minLuminance.value = 1.0f;
                exposure.maxLuminance.value = 1.0f;
                exposure.keyValue.value = 9.45f * CFG.Graphic.ExposureScale;
            }
            else if (TryCast(setting, out Bloom bloom))
            {
                bloom.intensity.value = 1.8f * CFG.Emission.BloomIntensity;
                bloom.diffusion.value = 6.25f * CFG.Emission.BloomSpread;
                bloom.anamorphicRatio.value = -0.25f;
                bloom.dirtIntensity.value = 0.75f;
                bloom.threshold.value = CFG.DEBUG.V2;
                bloom.softKnee.value = 0.0f;
                bloom.clamp.value = 600000.0f;
            }
            else if (TryCast(setting, out ColorGrading colorGrading))
            {
                colorGrading.tonemapper.value = Tonemapper.ACES;
                colorGrading.gradingMode.value = GradingMode.HighDefinitionRange;
                colorGrading.postExposure.value = 1.2f;
                colorGrading.contrast.value = 3.0f * CFG.Graphic.ContrastScale;
                colorGrading.temperature.value = -2.5f;
                colorGrading.saturation.value = 8.0f;

                //colorGrading.masterCurve.value.curve;
            }
        }
    }

    private static bool TryCast<F, T>(F from, out T to) where F : Il2CppObjectBase where T : Il2CppObjectBase
    {
        to = from.TryCast<T>();
        return to != null;
    }
}
