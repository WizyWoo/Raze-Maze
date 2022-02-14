using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TrapController : MonoBehaviourPunCallbacks , IPunObservable
{

    //When making a trap script make sure that you Inherit from this script, you can override the TrapPlaced and ActivateTrap functions, however they are not needed for it to work
    //The ID is set automatically based on where the prefab is located in the PlayerWeaponControllers prefab array

    //note to self, send and receive in same order

    public int TrapID;
    public float Damage;
    private bool trapPlaced, trapActive;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if(stream.IsWriting)
        {

            stream.SendNext(TrapID);
            stream.SendNext(trapPlaced);
            stream.SendNext(trapActive);

        }
        else if(stream.IsReading)
        {

            TrapID = (int)stream.ReceiveNext();
            trapPlaced = (bool)stream.ReceiveNext();
            trapActive = (bool)stream.ReceiveNext();

        }

    }

    public virtual void TrapPlaced(float activationDelay, int _trapID)
    {

        TrapID = _trapID;
        Invoke("ActivateTrap", activationDelay);
        trapPlaced = true;

    }

    public virtual void ActivateTrap()
    {

        trapActive = true;

    }

}
