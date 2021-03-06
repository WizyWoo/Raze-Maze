using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEditor.XR.LegacyInputHelpers;
using UnityEngine.SpatialTracking;
using FMODUnity;

public class TerminateLocalsVR : MonoBehaviourPunCallbacks
{

    public GameObject[] DestroyThis;
    public PlayerInteraction PI1, PI2;
    public VrHandsController Hand1, Hand2;
    public LineRenderer Line1, Line2;
    public TrackedPoseDriver[] PoseDrivers;
    public VRCamPos CamPos;
    public StudioListener FmodListener;


    private void Awake()
    {

        if(!photonView.IsMine)
        {

            foreach (GameObject go in DestroyThis)
            {

                Destroy(go);
                
            }

            Destroy(gameObject.GetComponent<Rigidbody>());
            Destroy(gameObject.GetComponent<PlayerItemController>());
            Destroy(gameObject.GetComponent<VrPlayerController>());
            Destroy(gameObject.GetComponent<CameraOffset>());
            Destroy(PI1);
            Destroy(PI2);
            Destroy(Hand1);
            Destroy(Hand2);
            Destroy(Line1);
            Destroy(Line2);
            Destroy(CamPos);
            Destroy(FmodListener);


            foreach (TrackedPoseDriver _driver in PoseDrivers)
            {

                Destroy(_driver);
                
            }

            Destroy(gameObject.GetComponentInChildren<Camera>());
            
        }

        Destroy(this);

    }

}
