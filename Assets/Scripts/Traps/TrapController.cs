using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{

    public int TrapID;

    public virtual void TrapPlaced(float activationDelay)
    {

        Invoke("ActivateTrap", activationDelay);

    }

    public virtual void ActivateTrap()
    {



    }

}
