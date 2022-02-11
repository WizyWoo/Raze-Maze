using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TrapController : MonoBehaviourPunCallbacks
{

    public int TrapID;
    public int Damage;

    public virtual void TrapPlaced(float activationDelay, int _trapID)
    {

        TrapID = _trapID;
        Invoke("ActivateTrap", activationDelay);
        Debug.Log("Trap placed " + TrapID + " activating in: " + activationDelay);

    }

    public virtual void ActivateTrap()
    {



    }

}
