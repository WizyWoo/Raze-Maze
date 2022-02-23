using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRCamPos : MonoBehaviour
{

    public CapsuleCollider CapCol;
    public Transform CamTransform;

    private void Start()
    {

        if(!CapCol)
            CapCol = gameObject.GetComponent<CapsuleCollider>();

    }

    private void Update()
    {

        transform.position = CamTransform.position;

        CapCol.height = CamTransform.localPosition.y;
        CapCol.center = new Vector3(0, -(CapCol.height / 2), 0);

    }

}
