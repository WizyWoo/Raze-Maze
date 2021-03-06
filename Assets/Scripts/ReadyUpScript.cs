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

        public GameObject chunkGenerator, Player;
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
                Player = _player.root.gameObject;        

                if(_player.root.GetComponent<PlayerManager>().colorId == 0)
                {
                     for (int i = 0; i < _player.root.GetComponent<PlayerManager>().playerColors.Length; i++)
                    {   
                        int colorToAssign = Random.Range(1, 4);
                        if(!_player.root.GetComponent<PlayerManager>().usedColors.Contains(colorToAssign))
                           _player.root.GetComponent<PlayerManager>().colorId = colorToAssign;
                    }   
                }
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
            {
                readyText.gameObject.SetActive(false);
                Player.GetComponentInChildren<PlayerItemController>().DropItem(0);
                Player.GetComponent<PlayerManager>().Heal();
                chunkGenerator.SetActive(true);
            }
        }
    }
}

