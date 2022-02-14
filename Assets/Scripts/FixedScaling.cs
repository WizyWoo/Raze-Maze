using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedScaling : MonoBehaviour
{

    [SerializeField]
    private bool KeepTransformScale;
    [SerializeField]
    private Vector3 ScaleToKeep;

    private void Start()
    {

        if(KeepTransformScale)
            ScaleToKeep = transform.lossyScale;

    }

    private void Update()
    {

        if(transform.lossyScale != ScaleToKeep)
            transform.localScale = new Vector3(ScaleToKeep.x * (ScaleToKeep.x / transform.lossyScale.x), ScaleToKeep.y * (ScaleToKeep.y / transform.lossyScale.y), ScaleToKeep.z * (ScaleToKeep.z / transform.lossyScale.z));

    }

}
