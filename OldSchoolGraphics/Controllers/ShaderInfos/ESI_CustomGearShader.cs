using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OldSchoolGraphics.Controllers.ShaderInfos;
internal sealed class ESI_CustomGearShader : EmissiveShaderInfo
{
    //PATH: Cell/Player/CustomGearShader
    //_EmissiveColor

    protected override string ShaderAssetPath => "Cell/Player/CustomGearShader";

    protected override bool TryCreateInfo(Material material, out EmissionInfo info)
    {
        info = EmissionInfo.Create(material, Prop_EmissiveColor);
        return true;
    }
}
