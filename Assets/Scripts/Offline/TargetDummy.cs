using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummy : MonoBehaviour , IHit
{

    public MeshRenderer Ren;
    public Material HitMat;
    public float timer;
    private Material originMat;

    private void Start()
    {

        originMat = Ren.material;

    }

    public void Damage(float _damageAmount)
    {

        timer = 0.4f;

    }

    private void Update()
    {

        if(timer > 0)
        {

            timer -= Time.deltaTime;
            Ren.material = HitMat;

        }
        else
            Ren.material = originMat;

    }

}
