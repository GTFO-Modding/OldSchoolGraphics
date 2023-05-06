using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OldSchoolGraphics.Controllers.ShaderInfos;
internal sealed class ESI_GearShader : EmissiveShaderInfo
{
    //PATH: Cell/Player/GearShader
    //KEYWORD: ENABLE_EMISSIVE
    //_EmissiveColor

    protected override string ShaderAssetPath => "Cell/Player/GearShader";

    protected override bool IsEmissiveMaterial(Material material)
    {
        return material.IsKeywordEnabled("ENABLE_EMISSIVE");
    }

    protected override bool TryCreateInfo(Material material, out EmissionInfo info)
    {
        info = EmissionInfo.Create(material, Prop_EmissiveColor);
        return true;
    }
}
