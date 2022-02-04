using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{

    public int TrapID;

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
