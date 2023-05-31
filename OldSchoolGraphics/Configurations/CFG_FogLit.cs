using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldSchoolGraphics.Configurations;
internal sealed class CFG_FogLit
{
    private const string SECTION = "3. Fog Lit System";

    public float FogBoost => _FogBoost.Value;
    public float Indirect => _Indirect.Value;
    public float IndirectReflection => _IndirectReflection.Value;
    public float SpotColorMaxCap => _SpotColorMaxCap.Value;
    public float SpotColorScale => _SpotColorScale.Value;
    public float PointColorMaxCap => _PointColorMaxCap.Value;
    public float PointColorScale => _PointColorScale.Value;
    public float EffectColorScale => _EffectColorScale.Value;
    public float LitAdjustment_MaxDensity => _LitAdjustment_MaxDensity.Value;
    public float LitAdjustment_MinDensity => _LitAdjustment_MinDensity.Value;
    public float LitAdjustment_DensityBonusAmount => _LitAdjustment_DensityBonusAmount.Value;
    public float LitAdjustment_IntensityToRangeWeight => _LitAdjustment_IntensityToRangeWeight.Value;

    private ConfigEntry<float> _FogBoost;
    private ConfigEntry<float> _Indirect;
    private ConfigEntry<float> _IndirectReflection;
    private ConfigEntry<float> _SpotColorMaxCap;
    private ConfigEntry<float> _SpotColorScale;

    private ConfigEntry<float> _PointColorMaxCap;
    private ConfigEntry<float> _PointColorScale;

    private ConfigEntry<float> _EffectColorScale;

    private ConfigEntry<float> _LitAdjustment_MaxDensity;
    private ConfigEntry<float> _LitAdjustment_MinDensity;
    private ConfigEntry<float> _LitAdjustment_DensityBonusAmount;
    private ConfigEntry<float> _LitAdjustment_IntensityToRangeWeight;

    internal void Initialize(ConfigFile cfg)
    {
        _FogBoost = cfg.Bind(SECTION, "Fog Density Boost", 0.55f);
        _Indirect = cfg.Bind(SECTION, "Indirect Intensity", 0.525f);
        _IndirectReflection = cfg.Bind(SECTION, "Indirect Reflection", 0.16f);

        _SpotColorMaxCap = cfg.Bind(SECTION, "Spot Light Intensity Max Cap", 0.0175f);
        _SpotColorScale = cfg.Bind(SECTION, "Spot Light Color Scale", 1.0f);

        _PointColorMaxCap = cfg.Bind(SECTION, "Point Light Intensity Max Cap", 0.0265f);
        _PointColorScale = cfg.Bind(SECTION, "Point Light Color Scale", 1.0f);

        _EffectColorScale = cfg.Bind(SECTION, "Effect Light Color Scale", 9.0f);

        _LitAdjustment_MaxDensity = cfg.Bind(SECTION, "(ADV) LitAdjust / MaxLit FogDensity", 0.0f);
        _LitAdjustment_MinDensity = cfg.Bind(SECTION, "(ADV) LitAdjust / MinLit FogDensity", 0.05f);
        _LitAdjustment_DensityBonusAmount = cfg.Bind(SECTION, "(ADV) LitAdjust / Density Bonus Amount", 0.001f);
        _LitAdjustment_IntensityToRangeWeight = cfg.Bind(SECTION, "(ADV) LitAdjust / Intensity To Range Weight", 15.0f);
    }
}
