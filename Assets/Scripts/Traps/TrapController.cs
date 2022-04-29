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
    public bool TrapPlaced, TrapActived, Used;
    public LayerMask TrapMask, HitMask;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if(stream.IsWriting)
        {

            stream.SendNext(TrapID);
            stream.SendNext(TrapPlaced);
            stream.SendNext(TrapActived);
            stream.SendNext(Used);

        }
        else if(stream.IsReading)
        {

            TrapID = (int)stream.ReceiveNext();
            TrapPlaced = (bool)stream.ReceiveNext();
            TrapActived = (bool)stream.ReceiveNext();
            Used = (bool)stream.ReceiveNext();

        }

    }

    private void Awake()
    {

        TrapMask = ~(1 << LayerMask.NameToLayer("Interactables"));
        HitMask = ((1 << LayerMask.NameToLayer("Player")) + (1 << LayerMask.NameToLayer("TakesDamage")));

    }

    public virtual void OnTrapPlaced(float activationDelay, int _trapID)
    {

        TrapID = _trapID;
        Invoke("ActivateTrap", activationDelay);
        TrapPlaced = true;

    }

    public virtual void ActivateTrap()
    {

        TrapActived = true;

    }

}
