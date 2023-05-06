using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OldSchoolGraphics.Controllers.ShaderInfos;
internal sealed class ESI_CMSWorldProp : EmissiveShaderInfo
{
    //PATH: GTFO/CMS_WorldProp
    //_EmissiveColor
    //_EnableEmissive

    protected override string ShaderAssetPath => "GTFO/CMS_WorldProp";

    protected override bool IsEmissiveMaterial(Material material)
    {
        return material.GetFloat(Prop_EnableEmissive) >= 1.0f;
    }

    protected override bool TryCreateInfo(Material material, out EmissionInfo info)
    {
        info = EmissionInfo.Create(material, Prop_EmissiveColor);
        return true;
    }
}
