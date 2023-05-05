using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OldSchoolGraphics.Inject;
[HarmonyPatch(typeof(PostMicroScratchLoader), nameof(PostMicroScratchLoader.Start))]
internal class Inject_MicroScratch
{
    static void Postfix()
    {
        Shader.SetGlobalTexture("_GlassMicroFacetsA", Texture2D.blackTexture);
    }
}
