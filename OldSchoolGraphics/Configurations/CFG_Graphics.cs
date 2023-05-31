using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldSchoolGraphics.Configurations;
internal sealed class CFG_Graphics
{
    private const string SECTION = "1. Graphics";

    public float ExposureScale => _ExposureScale.Value;
    public float NoiseScale => _NoiseScale.Value;
    public float DitherScale => _DitherScale.Value;
    public float ContrastScale => _ContrastFactor.Value;
    public float DustScale => _DustScale.Value;
    public float ColorSaturation => _ColorSaturation.Value;
    public float LiftLevel => _LiftLevel.Value;
    public float GammaLevel => _GammaLevel.Value;
    public float GainLevel => _GainLevel.Value;
    public bool ForceOffWetness => _ForceOffWetness.Value;

    private ConfigEntry<float> _ExposureScale;
    private ConfigEntry<float> _NoiseScale;
    private ConfigEntry<float> _DitherScale;
    private ConfigEntry<float> _ContrastFactor;
    private ConfigEntry<float> _DustScale;
    private ConfigEntry<float> _ColorSaturation;
    private ConfigEntry<float> _LiftLevel;
    private ConfigEntry<float> _GammaLevel;
    private ConfigEntry<float> _GainLevel;
    private ConfigEntry<bool> _ForceOffWetness;
    //private ConfigEntry<bool> _UsingLegacyFog;

    internal void Initialize(ConfigFile cfg)
    {
        _ExposureScale = cfg.Bind(SECTION, "Exposure Scale", 1.95f);
        _NoiseScale = cfg.Bind(SECTION, "Noise Scale", 0.85f);
        _DitherScale = cfg.Bind(SECTION, "Dither Scale", 0.0f);
        _ContrastFactor = cfg.Bind(SECTION, "Contrast Factor", 5.0f);
        _DustScale = cfg.Bind(SECTION, "Dust Particle Scale", 0.6f);
        _ColorSaturation = cfg.Bind(SECTION, "Color Saturation (Stronger Color)", -6.5f, "Value Range (-100.0 ~ 100.0)");
        _LiftLevel = cfg.Bind(SECTION, "Lift Level (Dark Color)", -0.06f);
        _GammaLevel = cfg.Bind(SECTION, "Gamma Level (Mid Color)", -0.17f);
        _GainLevel = cfg.Bind(SECTION, "Gain Level (Bright Color)", 0.4f);
        _ForceOffWetness = cfg.Bind(SECTION, "Force Off Wetness", true);
        //_UsingLegacyFog = cfg.Bind(GRAPHIC, "Use Legacy Fog", true);
    }
}
