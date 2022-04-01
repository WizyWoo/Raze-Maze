using UnityEngine;
using System.Collections;

public class KeyScript : MonoBehaviour, IInteractable
{
    private bool inTrigger;

    void OnTriggerEnter(Collider other)
    {
        inTrigger = true;
    }

    void OnTriggerExit(Collider other)
    {
        inTrigger = false;
    }

    public void Activate(Transform _player)
    {
        if (inTrigger)
        {
            DoorScript.doorKey = true;
            Destroy(this.gameObject);
        }
    }
}
