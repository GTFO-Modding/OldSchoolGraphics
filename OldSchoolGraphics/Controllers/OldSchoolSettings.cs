using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.InteropTypes;
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
    public const float EMISSION_MULT = 2.0f;

    public static void ApplyPPSettings(FPSCamera fpsCam)
    {
        fpsCam.SetBloomEnabled(true);

        var beProfile = fpsCam.postProcessing.m_ppBehavior.profile;
        beProfile.bloom.enabled = true;

        var volProfile = fpsCam.postProcessing.m_ppVolume.profile;

        foreach (var setting in volProfile.settings)
        {
            if (TryCast(setting, out AutoExposure exposure))
            {
                exposure.maxLuminance.value = 1.0f;
                exposure.minLuminance.value = 1.25f;
                exposure.keyValue.value = 2.02f * CFG.ExposureScale.Value;
            }
            else if (TryCast(setting, out Bloom bloom))
            {
                bloom.intensity.value = 3.63f * CFG.BloomScale.Value;
                bloom.anamorphicRatio.value = -0.7f;
                bloom.dirtIntensity.value = 0.1f;
                bloom.diffusion.value = 0.05f;
                bloom.threshold.value = 0.1f;
                bloom.softKnee.value = 0.65f;
                bloom.clamp.value = 120.0f;
            }
            else if (TryCast(setting, out ColorGrading colorGrading))
            {
                colorGrading.postExposure.value = 2.31f;
                colorGrading.contrast.value = 35.5f * CFG.ContrastFactor.Value;
            }
        }
    }

    private static bool TryCast<F, T>(F from, out T to) where F : Il2CppObjectBase where T : Il2CppObjectBase
    {
        to = from.TryCast<T>();
        return to != null;
    }
}
