using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Redified : MonoBehaviour
{
    private float redFlashTime = 1f;
    private Color originalColor;

    public Renderer ren;

    private void Start() 
    {
        originalColor = ren.material.color;
    }

    private void FlashRed()
    {
        ren.material.color = Color.red;

        Invoke("ResetColor", redFlashTime);
    }

    private void ResetColor()
    {
        ren.material.color = originalColor;
    }

}
