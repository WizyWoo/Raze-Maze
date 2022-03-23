using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Com.MyCompany.MyGame
{
    public class ReadyUpScript : MonoBehaviourPunCallbacks, IInteractable
    {
        // Start is called before the first frame update
        [SerializeField]
        private int playersInRoom, playerReadied;

        public GameObject chunkGenerator;
        private bool locked;

        // Update is called once per frame
        void Update()
        {
            playersInRoom = PhotonNetwork.CurrentRoom.PlayerCount;
        }


        public void Activate(Transform _player)
        {
            if (!locked)
            {
                PhotonView photonView = gameObject.GetPhotonView();

                photonView.RPC("ClickedByPlayer", RpcTarget.MasterClient);
            }        
        }

        [PunRPC]
        private void ClickedByPlayer()
        {
            playerReadied++;

            if (playersInRoom == playerReadied)
                photonView.RPC("ShitPoopiePissBaby", RpcTarget.All);
        }

        [PunRPC]
        private void ShitPoopiePissBaby()
        {

            chunkGenerator.SetActive(true);

        }

    }
}

