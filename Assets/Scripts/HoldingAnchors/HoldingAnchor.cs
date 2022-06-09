using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HoldingAnchor : MonoBehaviourPunCallbacks
{

    [Tooltip("If this is not the main anchor, do not link an anchor here")]
    public Transform LinkedAnchorTransform;
    public PhotonView PView;
    public bool IsHeld;
    [Tooltip("Don't assign this field")]
    public HoldingAnchor AttachedMainAnchor;
    [Tooltip("The object will rotate from this anchor if it is linked to another anchor"), SerializeField]
    private bool mainAnchor;
    public Transform handTransform;
    public Rigidbody rb;
    public Collider[] Colliders;
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

        if(!mainAnchor)
        {

            originPos = transform.localPosition;

        }

    }

    public virtual HoldingAnchor Grabbed(Transform _grabbedBy)
    {

        PView.TransferOwnership(_grabbedBy.transform.root.GetComponent<PhotonView>().ViewID);

        IsHeld = true;
        handTransform = _grabbedBy;

        if(rb)
            rb.isKinematic = true;

        return this;
        
    }

    public virtual void Released()
    {

        IsHeld = false;
        handTransform = null;

        if(rb)
            rb.isKinematic = false;

        if(!mainAnchor)
        {

            transform.localPosition = originPos;
            
        }

    }

    private void Update()
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
