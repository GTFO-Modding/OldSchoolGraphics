using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldSchoolGraphics.Configurations;
internal class CFG_User
{
    private const string SECTION = "3. User Preferences / Minor";

    public MeleeOverride MeleeType => _MeleeType.Value;
    public float FlashlightSwayFactor => _FlashlightSwayFactor.Value;

    private ConfigEntry<MeleeOverride> _MeleeType;
    private ConfigEntry<float> _FlashlightSwayFactor;

    internal void Initialize(ConfigFile cfg)
    {
        _MeleeType = cfg.Bind(SECTION, "Hammer Model", MeleeOverride.Default);
        _FlashlightSwayFactor = cfg.Bind(SECTION, "Flashlight Sway Movement", 0.25f, "(0.0 - 1.0) Lower = less movement from center");
    }
}

internal enum MeleeOverride
{
    Default,
    Gavel,
    Maul,
    Sledgehammer,
    Mallet
}
