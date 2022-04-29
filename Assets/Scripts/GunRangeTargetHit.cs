using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRangeTargetHit : MonoBehaviour, IHit
{
    public void Damage(float damageAmount)
    {
        Debug.LogError("PApi");
        StopCoroutine(HitMarkerActive());

        StartCoroutine(HitMarkerActive());
    }

    IEnumerator HitMarkerActive()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

         gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}
