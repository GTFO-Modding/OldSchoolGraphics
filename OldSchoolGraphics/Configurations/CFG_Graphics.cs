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
    public float ContrastScale => _ContrastFactor.Value;
    public float DustScale => _DustScale.Value;
    public float ColorSaturation => _ColorSaturation.Value;
    public float ColorTemperature => _ColorTemperature.Value;
    public float ColorTint => _ColorTint.Value;

    private ConfigEntry<float> _ExposureScale;
    private ConfigEntry<float> _NoiseScale;
    private ConfigEntry<float> _ContrastFactor;
    private ConfigEntry<float> _DustScale;
    private ConfigEntry<float> _ColorSaturation;
    private ConfigEntry<float> _ColorTemperature;
    private ConfigEntry<float> _ColorTint;
    //private ConfigEntry<bool> _UsingLegacyFog;

    internal void Initialize(ConfigFile cfg)
    {
        _ExposureScale = cfg.Bind(SECTION, "Exposure Scale", 1.0f);
        _NoiseScale = cfg.Bind(SECTION, "Noise Scale", 1.0f);
        _ContrastFactor = cfg.Bind(SECTION, "Contrast Factor", 1.0f);
        _DustScale = cfg.Bind(SECTION, "Dust Particle Scale", 0.6f);
        _ColorSaturation = cfg.Bind(SECTION, "Color Saturation (Stronger Color)", -5.5f, "Value Range (-100.0 ~ 100.0)");
        _ColorTemperature = cfg.Bind(SECTION, "Color Temperature (Blue-Red)", 0.0f, "Value Range (-100.0 ~ 100.0)");
        _ColorTint = cfg.Bind(SECTION, "Color Tint (Green-Purple)", 0.0f, "Value Range (-100.0 ~ 100.0)");
        //_UsingLegacyFog = cfg.Bind(GRAPHIC, "Use Legacy Fog", true);
    }
}
