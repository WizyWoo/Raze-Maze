using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawnpoint : MonoBehaviour
{
    public GameObject randomWeaponPickup;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(randomWeaponPickup,transform.position,Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
