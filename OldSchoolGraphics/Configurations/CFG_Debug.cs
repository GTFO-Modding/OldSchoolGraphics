using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldSchoolGraphics.Configurations;
internal sealed class CFG_Debug
{
    private const string SECTION = "0. Debug";

#if DEBUG
    public float V1 { get; private set; } = 1.0f;
    public float V2 { get; private set; } = 1.0f;
    public float V3 { get; private set; } = 1.0f;

    private ConfigEntry<float> _V1;
    private ConfigEntry<float> _V2;
    private ConfigEntry<float> _V3;
#endif

    [Conditional("DEBUG")]
    internal void Initialize(ConfigFile cfg)
    {
#if DEBUG
        _V1 = cfg.Bind(SECTION, "Value1", 1.0f);
        _V2 = cfg.Bind(SECTION, "Value2", 1.0f);
        _V3 = cfg.Bind(SECTION, "Value3", 1.0f);
        UpdateValue();
#endif
    }

    [Conditional("DEBUG")]
    internal void UpdateValue()
    {
#if DEBUG
        V1 = _V1.Value;
        V2 = _V2.Value;
        V3 = _V3.Value;
#endif
    }
}
