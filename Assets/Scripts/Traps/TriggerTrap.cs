// To be placed on the trigger of a trap to spawn in the effect that trap gives. I.E spawn in the baloon that carries the player

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTrap : TrapController
{

    [SerializeField]
    private GameObject _objectToSpawn;
    private LayerMask _triggerTag = 8;
    private bool _hasBeenTriggered;
    private void OnTriggerEnter(Collider other) {
        if(((1<<other.gameObject.layer) & _triggerTag) != 0 && !_hasBeenTriggered){
            _hasBeenTriggered = true;
            GameObject trap = Instantiate(_objectToSpawn, transform.position, Quaternion.identity);
            other.attachedRigidbody.transform.parent = trap.transform;
            if(trap.TryGetComponent(out BalloonFunctionality bf)){
                bf.positions.AddRange(other.attachedRigidbody.gameObject.GetComponent<PlayerPositionLog>().playerPosHistory);
            }

            // Removes the trap
            Destroy(gameObject);
        }
    }
}
