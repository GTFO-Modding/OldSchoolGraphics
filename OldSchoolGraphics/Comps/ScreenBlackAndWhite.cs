using GTFO.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

namespace OldSchoolGraphics.Comps;
internal class ScreenBlackAndWhite : MonoBehaviour
{
    public static ScreenBlackAndWhite Instance { get; private set; }

    private Material _BWMat;
    private Shader _BWShader;
    private CommandBuffer _Cmd;

    private readonly List<Transform> _TrackingTransforms = new();

    private void Start()
    {
        Instance = this;

        _BWMat = AssetAPI.GetLoadedAsset<Material>("Assets/OSG/BAWMat.mat");
        _BWShader = AssetAPI.GetLoadedAsset<Shader>("Assets/OSG/BlackAndWhite.shader");
        //_BWMat = new Material(Shader.Find("Unlit/Color"));
        //_BWMat.SetColor("_Color", Color.black);

        _Cmd = new CommandBuffer();
        _Cmd.name = "loadout player black and white command";

        GetComponent<Camera>().AddCommandBuffer(CameraEvent.AfterGBuffer, _Cmd);
    }

    public void UpdateCommand()
    {
        _Cmd.Clear();
        foreach (var root in _TrackingTransforms)
        {
            foreach (var renderer in root.GetComponentsInChildren<MeshRenderer>())
            {
                if (renderer.shadowCastingMode == ShadowCastingMode.ShadowsOnly)
                    continue;

                if (renderer.shadowCastingMode == ShadowCastingMode.Off)
                    continue;

                if (!renderer.enabled)
                    continue;

                renderer.gameObject.layer = LayerManager.LAYER_DEBRIS;

                var newMat = new Material(_BWShader);
                newMat.mainTexture = renderer.material.mainTexture;
                _Cmd.DrawRenderer(renderer, newMat);
            }

            foreach (var renderer in root.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                if (renderer.shadowCastingMode == ShadowCastingMode.ShadowsOnly)
                    continue;

                if (renderer.shadowCastingMode == ShadowCastingMode.Off)
                    continue;

                if (!renderer.enabled)
                    continue;

                renderer.gameObject.layer = LayerManager.LAYER_DEBRIS;
                renderer.updateWhenOffscreen = true;

                var newMat = new Material(_BWShader);
                newMat.mainTexture = renderer.material.mainTexture;
                _Cmd.DrawRenderer(renderer, _BWMat);
            }
        }
    }

    public void AddTrackingTransform(Transform playerModelRoot)
    {
        _TrackingTransforms.Add(playerModelRoot);
    }

    public void RemoveTrackingTransform(Transform playerModelRoot)
    {
        var idx = _TrackingTransforms.FindIndex(x => x.GetInstanceID() == playerModelRoot.GetInstanceID());
        if (idx >= 0)
        {
            _TrackingTransforms.RemoveAt(idx);
        }
    }
}
