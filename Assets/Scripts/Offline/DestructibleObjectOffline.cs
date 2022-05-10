using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObjectOffline : MonoBehaviour , IHit
{

    public float ObjectHealth;
    public SkinnedMeshRenderer Ren;
    public Material HitMat;
    private float timer;
    private Material originMat;

    private void Start()
    {

        originMat = Ren.material;

    }

    public void Damage(float _damageAmount)
    {

        ObjectHealth -= _damageAmount;
        timer = 0.4f;

        if(ObjectHealth <= 0)
        {

            Destroy(gameObject);

        }

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
