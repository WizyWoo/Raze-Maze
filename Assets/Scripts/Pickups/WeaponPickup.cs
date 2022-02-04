using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WeaponPickup : MonoBehaviourPunCallbacks , IInteractable
{

    public int WeaponID;

    public void Activate(Transform player)
    {

        player.GetComponent<PlayerWeaponController>().PickedUpWeapon(WeaponID);

        PhotonNetwork.Destroy(gameObject);

    }

}