using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldSchoolGraphics.Configurations;
internal sealed class CFG_Emissions
{
    private const string SECTION = "2. Emissions";

    public float BloomIntensity => _BloomIntensity.Value;
    public float BloomSpread => _BloomSpread.Value;
    public float ObjectBloomScale => _ObjectBloomScale.Value;
    public float EnemyGlowScale => _EnemyGlowScale.Value;

    private ConfigEntry<float> _BloomIntensity;
    private ConfigEntry<float> _BloomSpread;
    private ConfigEntry<float> _ObjectBloomScale;
    private ConfigEntry<float> _EnemyGlowScale;

    internal void Initialize(ConfigFile cfg)
    {
        _BloomIntensity = cfg.Bind(SECTION, "Bloom Intensity", 1.0f);
        _BloomSpread = cfg.Bind(SECTION, "Bloom Spread", 1.0f);
        _ObjectBloomScale = cfg.Bind(SECTION, "Object Bloom Scale", 1.0f);
        _EnemyGlowScale = cfg.Bind(SECTION, "Sleeper Glow Scale", 1.0f);
    }
}
