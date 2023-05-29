﻿using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldSchoolGraphics.Configurations;
internal sealed class CFG_Emissions
{
    private const string SECTION = "2. Emissions";

    public float FlashlightIntenisty => _FlashlightIntensity.Value;
    public float PlayerAmbientIntensity => _PlayerAmbientIntensity.Value;
    public float BloomIntensity => _BloomIntensity.Value;
    public float BloomSpread => _BloomSpread.Value;
    public float BloomThreshold => _BloomThreshold.Value;
    public float ObjectBloomScale => _ObjectBloomScale.Value;
    public float EnemyGlowScale => _EnemyGlowScale.Value;

    private ConfigEntry<float> _FlashlightIntensity;
    private ConfigEntry<float> _PlayerAmbientIntensity;
    private ConfigEntry<float> _BloomIntensity;
    private ConfigEntry<float> _BloomSpread;
    private ConfigEntry<float> _BloomThreshold;
    private ConfigEntry<float> _ObjectBloomScale;
    private ConfigEntry<float> _EnemyGlowScale;

    internal void Initialize(ConfigFile cfg)
    {
        _FlashlightIntensity = cfg.Bind(SECTION, "Flashlight Intensity", 1.0f);
        _PlayerAmbientIntensity = cfg.Bind(SECTION, "Player Ambient Intensity", 1.0f);
        _BloomIntensity = cfg.Bind(SECTION, "Bloom Intensity", 0.7f);
        _BloomSpread = cfg.Bind(SECTION, "Bloom Spread", 9.0f, "Value Range (0.0 ~ 10.0)");
        _BloomThreshold = cfg.Bind(SECTION, "Bloom Threshold", 0.21f);
        _ObjectBloomScale = cfg.Bind(SECTION, "Object Bloom Scale", 1.0f);
        _EnemyGlowScale = cfg.Bind(SECTION, "Sleeper Glow Scale", 1.0f);
    }
}
