using ExteriorRendering;
using GTFO.API;
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
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityStandardAssets.ImageEffects;
using Bloom = UnityEngine.Rendering.PostProcessing.Bloom;

namespace OldSchoolGraphics.Controllers;
internal static class OldSchoolSettings
{
    public const float EMISSION_MULT = 1.65f;

    private static ScreenGrains _ScreenGrains;
    private static FogPrelit _FogPrelit;

    public static void AddGraphicComponents(FPSCamera fpsCam)
    {
        _ScreenGrains = fpsCam.gameObject.AddComponent<ScreenGrains>();
        _ScreenGrains.Setup();

        _FogPrelit = fpsCam.gameObject.AddComponent<FogPrelit>();
        _FogPrelit.Setup();
    }

    public static void ApplyPPSettings(FPSCamera fpsCam)
    {
        if (CFG.Graphic.ForceOffWetness)
        {
            Shader.SetGlobalFloat("_GlobalWetness", 0.0f);
        }

        Shader.SetGlobalFloat("_DitherAmount", CFG.Graphic.DitherScale);
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
            var prelit = fpsCam.PrelitVolume;
            prelit.mode = PreLitVolume.AccumulationMode.MultiPass;
            //prelit.FogPostBlur = 1;
            prelit.DensityBoost = CFG.FogLit.FogBoost;
            //prelit.DownsampleCount = 0;
            //prelit.FogShadowSamples = 2;
            //prelit.FogAACount = 2;

            //Indirect
            prelit.IndirectIntensity = CFG.FogLit.Indirect;
        }

        if (fpsCam.AmbientParticles != null)
        {
            var particle = fpsCam.AmbientParticles;
            var mat = particle.material;
            var factor = CFG.Graphic.DustScale;
            mat.SetFloat("_SizeMin", 0.002f * factor);
            mat.SetFloat("_SizeMax", 0.003f * factor);
        }

        if (fpsCam.m_amplifyOcclusion != null)
        {
            var amp = fpsCam.m_amplifyOcclusion;
            amp.BlurEnabled = false;
        }
        
        var sss = fpsCam.gameObject.GetComponent<SubsurfaceScattering.SSS>();
        if (sss != null)
        {
            sss.occlusionColoring = 0.0f;
        }
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
                exposure.keyValue.value = 11.3f * CFG.Graphic.ExposureScale;
            }
            else if (TryCast(setting, out Bloom bloom))
            {
                bloom.intensity.value = 0.99f * CFG.Emission.BloomIntensity;
                bloom.diffusion.value = CFG.Emission.BloomSpread;
                bloom.anamorphicRatio.value = -0.25f;
                bloom.dirtIntensity.value = 0.9f;
                bloom.threshold.value = CFG.Emission.BloomThreshold;
                bloom.softKnee.value = 0.0f;
                bloom.clamp.value = 600000.0f;
            }
            else if (TryCast(setting, out ColorGrading colorGrading))
            {
                colorGrading.gradingMode.value = GradingMode.HighDefinitionRange;
                colorGrading.tonemapper.value = Tonemapper.ACES;
                colorGrading.postExposure.value = 1.2f;
                colorGrading.contrast.value = 0.6f * CFG.Graphic.ContrastScale;
                colorGrading.saturation.value = CFG.Graphic.ColorSaturation;
                colorGrading.lift.value = new Vector4(1.0f, 1.0f, 1.0f, CFG.Graphic.LiftLevel);
                colorGrading.gamma.value = new Vector4(1.0f, 1.0f, 1.0f, CFG.Graphic.GammaLevel);
                colorGrading.gain.value = new Vector4(1.0f, 1.0f, 1.0f, CFG.Graphic.GainLevel);
            }
            else if (TryCast(setting, out Vignette vignette))
            {
                vignette.active = false;
                vignette.enabled.value = false;
                vignette.intensity.value = 0.0f;
            }
        }
    }

    private static bool TryCast<F, T>(F from, out T to) where F : Il2CppObjectBase where T : Il2CppObjectBase
    {
        to = from.TryCast<T>();
        return to != null;
    }
}
