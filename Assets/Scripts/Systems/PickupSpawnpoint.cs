using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawnpoint : MonoBehaviour
{
    public GameObject randomWeaponPickup;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("InstantiatePickup", 0.2f);
    }
    void InstantiatePickup(){
        Instantiate(randomWeaponPickup,transform.position,Quaternion.identity);

    }
}
