using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WeaponController : MonoBehaviourPunCallbacks
{

    public int WeaponID;
    public int Damage;

    public virtual void Thrown()
    {}

    public virtual void FireWeapon()
    {}

}