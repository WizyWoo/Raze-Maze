using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRangeTargetHit : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) 
    {
        StartCoroutine(HitMarkerActive());
    }

    IEnumerator HitMarkerActive()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

         gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}
