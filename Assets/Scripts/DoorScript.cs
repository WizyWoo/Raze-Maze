using UnityEngine;
using System.Collections;
using Photon.Pun;

public class DoorScript : MonoBehaviourPunCallbacks, IInteractable
{
    public bool open;
    private Animation anim;

    void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<KeyScript>(out KeyScript key))
        {
            if (anim != null)
                anim.Play();
        }
    }
}
