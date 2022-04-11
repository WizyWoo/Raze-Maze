using UnityEngine;
using System.Collections;
using Photon.Pun;

public class KeyScript : MonoBehaviourPunCallbacks, IInteractable
{
    private DoorScript door;

     public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                // We own this player: send the others our data
                stream.SendNext(door);
            }
            else
            {
                // Network player, receive data
                this.door = (DoorScript)stream.ReceiveNext();
            }
        }


    void OnCollisionEnter(Collision other)
    {
       if(other.gameObject.TryGetComponent<DoorScript>(out door))
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