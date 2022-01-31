using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PPCamFX : MonoBehaviour
{

    public Material ShaderMat;

    private void OnRenderImage(RenderTexture source, RenderTexture dest)
    {

        Graphics.Blit(source, dest, ShaderMat);

    }

}
