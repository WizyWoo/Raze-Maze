using UnityEngine;
using System.Collections;
using Photon.Pun;

public class KeyScript : MonoBehaviourPunCallbacks, IInteractable
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
           photonView.RPC("CallDoor", RpcTarget.All);
       }
    }

    [PunRPC]
    void CallDoor()
    {
        Debug.Log("IT ENTERED THE METHOD");
        door.DoorOpen();
    }
}