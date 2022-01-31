using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour , IInteractable
{

    public int WeaponID;

    public void Activate(Transform player)
    {

        player.GetComponent<PlayerWeaponController>().PickedUpWeapon(WeaponID);

        Destroy(gameObject);

    }

}