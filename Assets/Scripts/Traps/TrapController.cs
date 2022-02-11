using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TrapController : MonoBehaviourPunCallbacks , IPunObservable
{

    //When making a trap script make sure that you Inherit from this script, you can override the TrapPlaced and ActivateTrap functions, however they are not needed for it to work
    //The ID is set automatically based on where the prefab is located in the PlayerWeaponControllers prefab array

    public int TrapID;
    public float Damage;
    private bool trapPlaced, trapActivate;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if(stream.IsWriting)
        {

            stream.SendNext(TrapID);

        }
        else
        {

            Debug.Log("Hello I am mr " + (int)stream.ReceiveNext());
            this.TrapID = (int)stream.ReceiveNext();

        }

    }

    public virtual void TrapPlaced(float activationDelay, int _trapID)
    {

        TrapID = _trapID;
        Invoke("ActivateTrap", activationDelay);
        Debug.Log("Funnything: " + _trapID);

    }

    public virtual void ActivateTrap()
    {



    }

}
