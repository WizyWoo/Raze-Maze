using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPickup : MonoBehaviour , IInteractable
{

    public void Activate(Transform player)
    {

        Debug.Log(player.name + " picked up");

        PlayerWeaponController weaponController = player.GetComponent<PlayerWeaponController>();
        weaponController.PickedUpWeapon(Random.Range(1, weaponController.WeaponPrefabs.Length));

    }

}
