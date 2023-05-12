using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OldSchoolGraphics.Comps;
internal class DebugBehaviour : MonoBehaviour
{
    public static DebugBehaviour Current;

    public static RenderTexture Buffer1;
    public static RenderTexture Buffer2;

    void Start()
    {
        Current = this;
        
    }

    void OnGUI()
    {
        //var tex = Shader.GetGlobalTexture(PreLitVolume._LitVolume).Cast<RenderTexture>();
        //var tex2 = Shader.GetGlobalTexture(PreLitVolume._FogVolume).Cast<RenderTexture>();
        //GUI.Box(new Rect(0.0f, 0.0f, 200.0f, 600.0f), $"{tex.GetInstanceID()} {tex.dimension} / ({tex.width}, {tex.height}, {tex.volumeDepth})");
        //GUI.DrawTexture(new Rect(200.0f, 0.0f, 200.0f, 200.0f), tex);
    }
}
