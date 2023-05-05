using Il2CppInterop.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityStandardAssets.ImageEffects;
using Bloom = UnityEngine.Rendering.PostProcessing.Bloom;

namespace OldSchoolGraphics;
internal static class OldSchoolSettings
{
    public static void ApplyPPSettings(FPSCamera fpsCam)
    {
        fpsCam.SetBloomEnabled(true);

        var beProfile = fpsCam.postProcessing.m_ppBehavior.profile;
        beProfile.bloom.enabled = true;

        var volProfile = fpsCam.postProcessing.m_ppVolume.profile;
        
        foreach (var setting in volProfile.settings)
        {
            var classPtr = setting.GetIl2CppType().Pointer;
            if (classPtr == Il2CppType.Of<AutoExposure>().Pointer)
            {
                var autoexposure = setting.TryCast<AutoExposure>();
                autoexposure.maxLuminance.value = 1.0f;
                autoexposure.minLuminance.value = 1.25f;
                autoexposure.keyValue.value = 2.75f * CFG.ExposureScale.Value;
            }
            else if (classPtr == Il2CppType.Of<Bloom>().Pointer)
            {
                var bloom = setting.TryCast<Bloom>();
                bloom.intensity.value = 5.85f * CFG.BloomScale.Value;
                bloom.anamorphicRatio.value = -0.7f;
                bloom.dirtIntensity.value = 0.1f;
                bloom.diffusion.value = 0.05f;
                bloom.threshold.value = 0.1f;
                bloom.softKnee.value = 0.65f;
                bloom.clamp.value = 120.0f;
            }
        }
    }
}
