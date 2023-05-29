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
    public float SpotColorMaxCap => _SpotColorMaxCap.Value;
    public float SpotColorScale => _SpotColorScale.Value;
    public float PointColorMaxCap => _PointColorMaxCap.Value;
    public float PointColorScale => _PointColorScale.Value;
    public float EffectColorScale => _EffectColorScale.Value;

    private ConfigEntry<float> _FogBoost;
    private ConfigEntry<float> _Indirect;
    private ConfigEntry<float> _SpotColorMaxCap;
    private ConfigEntry<float> _SpotColorScale;

    private ConfigEntry<float> _PointColorMaxCap;
    private ConfigEntry<float> _PointColorScale;

    private ConfigEntry<float> _EffectColorScale;

    internal void Initialize(ConfigFile cfg)
    {
        _FogBoost = cfg.Bind(SECTION, "Fog Density Boost", 0.4f);
        _Indirect = cfg.Bind(SECTION, "Indirect Intensity", 0.0f);

        _SpotColorMaxCap = cfg.Bind(SECTION, "Spot Light Intensity Max Cap", 0.05f);
        _SpotColorScale = cfg.Bind(SECTION, "Spot Light Color Scale", 1.0f);

        _PointColorMaxCap = cfg.Bind(SECTION, "Point Light Intensity Max Cap", 0.05f);
        _PointColorScale = cfg.Bind(SECTION, "Point Light Color Scale", 1.0f);

        _EffectColorScale = cfg.Bind(SECTION, "Effect Light Color Scale", 8.0f);
    }
}
