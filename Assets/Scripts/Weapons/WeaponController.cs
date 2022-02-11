using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WeaponController : MonoBehaviourPunCallbacks
{

    //When making a weapon script make sure that you Inherit from this script, you can use the Thrown and FireWeapon functions as an on use event, however they are not needed for it to work
    //The ID is set automatically based on where the prefab is located in the PlayerWeaponControllers prefab array

    public int WeaponID;
    public float Damage;

    public virtual void Thrown()
    {}

    public virtual void FireWeapon()
    {}

}