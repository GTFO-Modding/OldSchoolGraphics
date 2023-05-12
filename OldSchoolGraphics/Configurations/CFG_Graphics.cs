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

    private ConfigEntry<float> _ExposureScale;
    private ConfigEntry<float> _NoiseScale;
    private ConfigEntry<float> _ContrastFactor;
    private ConfigEntry<float> _DustScale;
    //private ConfigEntry<bool> _UsingLegacyFog;

    internal void Initialize(ConfigFile cfg)
    {
        _ExposureScale = cfg.Bind(SECTION, "Exposure Scale", 1.0f);
        _NoiseScale = cfg.Bind(SECTION, "Noise Scale", 1.0f);
        _ContrastFactor = cfg.Bind(SECTION, "Contrast Factor", 1.0f);
        _DustScale = cfg.Bind(SECTION, "Dust Particle Scale", 0.6f);
        //_UsingLegacyFog = cfg.Bind(GRAPHIC, "Use Legacy Fog", true);
    }
}
