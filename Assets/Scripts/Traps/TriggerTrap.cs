// To be placed on the trigger of a trap to spawn in the effect that trap gives. I.E spawn in the baloon that carries the player

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTrap : MonoBehaviour
{

    [SerializeField]
    private GameObject _objectToSpawn;
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            GameObject trap = Instantiate(_objectToSpawn, transform.position, Quaternion.identity);
            other.transform.parent = trap.transform;

            // Removes the trap
            Destroy(gameObject);
        }
    }
}
