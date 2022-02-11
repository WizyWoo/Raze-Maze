using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WeaponController : MonoBehaviourPunCallbacks , IPunObservable
{

    //When making a weapon script make sure that you Inherit from this script, you can use the Thrown and FireWeapon functions as an on use event, however they are not needed for it to work
    //The ID is set automatically based on where the prefab is located in the PlayerWeaponControllers prefab array

    public int WeaponID;
    public float Damage;
    public bool Firing;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if(stream.IsWriting)
        {

            stream.SendNext(WeaponID);
            stream.SendNext(Firing);

        }
        else if(stream.IsReading)
        {

            WeaponID = (int)stream.ReceiveNext();
            Firing = (bool)stream.ReceiveNext();

        }

    }

    public virtual void Thrown()
    {}

    public virtual void FireWeapon()
    {}

}