using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OldSchoolGraphics.Controllers.ShaderInfos;
internal sealed class ESI_DisplayGearShader : EmissiveShaderInfo
{
    //PATH: Cell/Player/Display_GearShader
    //KEYWORD: _EMISSION
    //_EmissiveColor

    protected override string ShaderAssetPath => "Cell/Player/Display_GearShader";

    protected override bool IsEmissiveMaterial(Material material)
    {
        return material.IsKeywordEnabled("_EMISSION");
    }

    protected override bool TryCreateInfo(Material material, out EmissionInfo info)
    {
        info = EmissionInfo.Create(material, Prop_EmissiveColor);
        return true;
    }
}
