using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour , IInteractable
{

    public int WeaponID;

    public void Activate(Transform player)
    {

        Debug.Log(player.name + " picked up");
        player.GetComponent<PlayerWeaponController>().PickedUpWeapon(WeaponID);

    }

}