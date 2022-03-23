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
        private int readyCounter = 3;

        public TextMesh readyText;

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

                locked = true;
                readyText.text = "WAITING FOR OTHERS";
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
            StartCoroutine(PissBabyIsPooping());
        }

        private IEnumerator PissBabyIsPooping()
        {
            readyText.text = readyCounter.ToString();

            yield return new WaitForSeconds(1f);

            readyCounter--;

            if (readyCounter >= 0)
                StartCoroutine(PissBabyIsPooping());
            else
                chunkGenerator.SetActive(true);
        }
    }
}

