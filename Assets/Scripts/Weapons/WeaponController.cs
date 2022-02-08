using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WeaponController : MonoBehaviourPunCallbacks
{

    public int WeaponID;

    private void Start()
    {
        //transform.SetParent(GameObject.Find("HandPos").transform);
    }

    public virtual void Thrown()
    {}

    public virtual void FireWeapon()
    {}

}