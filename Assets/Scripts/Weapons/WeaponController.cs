using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using FMODUnity;

public class WeaponController : MonoBehaviourPunCallbacks , IPunObservable , IActivatable
{

    //When making a weapon script make sure that you Inherit from this script, you can use the Thrown and FireWeapon functions as an on use event, however they are not needed for it to work
    //The ID is set automatically based on where the prefab is located in the PlayerWeaponControllers prefab array

    public int WeaponID;
    [Tooltip("Keep in mind, the player has 1 health")]
    public float Damage;
    public bool Firing;
    public bool Used, ExplosiveUsed;
    public HoldingAnchor MainAnchor;
    public LayerMask WeaponMask, HitMask;
    public StudioEventEmitter SoundEmitter;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if(stream.IsWriting)
        {
            
            stream.SendNext(WeaponID);
            stream.SendNext(Firing);
            stream.SendNext(Used);

        }
        else if(stream.IsReading)
        {

            WeaponID = (int)stream.ReceiveNext();
            Firing = (bool)stream.ReceiveNext();
            Used = (bool)stream.ReceiveNext();

        }

    }

    private void Awake()
    {

        WeaponMask = ~(1 << LayerMask.NameToLayer("Interactables"));

        HitMask = ((1 << LayerMask.NameToLayer("Player")) + (1 << LayerMask.NameToLayer("TakesDamage")));

    }

    public bool HitMaskCheck(Transform _hit)
    {

        if(_hit.gameObject.layer == LayerMask.NameToLayer("Player") || _hit.gameObject.layer == LayerMask.NameToLayer("TakesDamage"))
            return true;
        else
            return false;

    }

    public virtual void Thrown()
    {}

    public virtual void Fire(bool _fire)
    {}

    public void Activate(bool _OnOff)
    {

        Fire(_OnOff);

    }

    public void PlaySound()
    {

        if(SoundEmitter)
        {

            SoundEmitter.Play();

        }

    }

}