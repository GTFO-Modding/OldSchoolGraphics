using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OldSchoolGraphics.Controllers.ShaderInfos;
internal sealed class ESI_Standard : EmissiveShaderInfo
{
    //PATH: Standard
    //KEYWORD: _EMISSION
    //_EmissionColor

    protected override string ShaderAssetPath => "Standard";

    protected override bool IsEmissiveMaterial(Material material)
    {
        return material.IsKeywordEnabled("_EMISSION");
    }

    protected override bool TryCreateInfo(Material material, out EmissionInfo info)
    {
        info = EmissionInfo.Create(material, Prop_EmissionColor);
        return true;
    }
}
