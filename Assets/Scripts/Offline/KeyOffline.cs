using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyOffline : MonoBehaviour
{
    private DoorScript door;

    private void Start() 
    {
        door = FindObjectOfType<DoorScript>();
    }

    void OnCollisionEnter(Collision other)
    {
       if(other.gameObject.TryGetComponent<DoorScript>(out DoorScript kevin))
       {
           kevin.DoorOpen();
       }
    }

}
