using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRangeTargetHit : MonoBehaviour, IHit
{

    public Material DummyMat;
    private Color originColor;

    private void Start()
    {

        originColor = DummyMat.GetColor("_DiffuseColor");

    }

    public void Damage(float damageAmount)
    {
        StopAllCoroutines();

        StartCoroutine(HitMarkerActive());
    }

    IEnumerator HitMarkerActive()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        DummyMat.SetColor("_DiffuseColor", Color.white);

        yield return new WaitForSeconds(1f);

        DummyMat.SetColor("_DiffuseColor", originColor);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}
