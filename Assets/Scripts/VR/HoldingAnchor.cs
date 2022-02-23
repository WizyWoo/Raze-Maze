using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingAnchor : MonoBehaviour
{

    [Tooltip("If this is not the main anchor, do not link an anchor here")]
    public Transform LinkedAnchorTransform;
    public bool IsHeld;
    [Tooltip("Don't assign this field")]
    public HoldingAnchor AttachedMainAnchor;
    [Tooltip("The object will rotate from this anchor if it is linked to another anchor"), SerializeField]
    private bool mainAnchor;
    private Transform handTransform;
    private Rigidbody rb;
    private Vector3 originPos;

    private void Awake()
    {

        if(LinkedAnchorTransform && mainAnchor)
        {

            LinkedAnchorTransform.GetComponent<HoldingAnchor>().AttachedMainAnchor = this;

        }

    }

    private void Start()
    {

        originPos = transform.position;
        rb = transform.root.GetComponent<Rigidbody>();

    }

    public void Grabbed(Transform _grabbedBy)
    {

        IsHeld = true;
        handTransform = _grabbedBy;

        if(rb)
            rb.isKinematic = true;
        
    }

    public void Released()
    {

        IsHeld = false;
        handTransform = null;

        if(rb)
            rb.isKinematic = false;

        transform.position = originPos;

    }

    private void FixedUpdate()
    {

        if(IsHeld && mainAnchor)
        {

            if(LinkedAnchorTransform)
            {

                transform.position = handTransform.position;

                if(LinkedAnchorTransform.GetComponent<HoldingAnchor>().IsHeld)
                {
                    
                    transform.LookAt(LinkedAnchorTransform, handTransform.up);

                }
                else
                {

                    transform.rotation = handTransform.rotation;

                }

            }
            else
            {

                transform.position = handTransform.position;
                transform.rotation = handTransform.rotation;

            }

        }
        else if(IsHeld && AttachedMainAnchor.IsHeld)
        {

            transform.position = handTransform.position;

        }

    }

}
