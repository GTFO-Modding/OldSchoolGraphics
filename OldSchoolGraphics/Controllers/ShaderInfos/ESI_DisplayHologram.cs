using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OldSchoolGraphics.Controllers.ShaderInfos;
internal sealed class ESI_DisplayHologram : EmissiveShaderInfo
{
    //PATH: Cell/Player/Display_Hologram
    //_EmissiveColor

    protected override string ShaderAssetPath => "Cell/Player/Display_Hologram";

    protected override bool TryCreateInfo(Material material, out EmissionInfo info)
    {
        info = EmissionInfo.Create(material, Prop_EmissiveColor);
        return true;
    }
}
