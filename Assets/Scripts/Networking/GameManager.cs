using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

namespace Com.MyCompany.MyGame
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        #region Public Fields

        public GameObject completeLevelUI;
        public Vector3 lastCheckpointPos;
        public static GameManager Instance;

        [Tooltip("The prefab to use for representing the player")]
        public string playerPrefabName;
        
        #endregion

        [SerializeField] private float levelTransitionDelay = 1f;
        public GameObject player;

        private List<PlayerManager> playerManagers;

        #region Photon Callbacks


        public static GameManager gameManager;
        

        private void Awake()
        {
            gameManager = this;
            playerManagers = new List<PlayerManager>();
        }

        /// <summary>
        /// Called when the local player left the room. We need to load the launcher scene.
        /// </summary>
        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }

        private void Update()
        {

            if(Input.GetKeyDown(KeyCode.G))
            {

                PhotonNetwork.Instantiate("PickupDroppedWeaponPrefab", Vector3.up * 5, Quaternion.identity);
                
            }

        }

        public override void OnPlayerEnteredRoom(Player other)
        {
            Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting


            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


               // LoadArena();
            }
        }


        public override void OnPlayerLeftRoom(Player other)
        {
            Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects


            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


                //LoadArena();
            }
        }


        #endregion

        #region Public Methods


        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
            PlayerManager.playerManager.someoneHasWon = false;
        }


        #endregion

        #region Private Methods

        void Start()
        {
            Instance = this;

            if (playerPrefabName == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
            }
            else
            {
                if (PlayerManager.LocalPlayerInstance == null)
                {
                    Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
                    // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                    PhotonNetwork.Instantiate(this.playerPrefabName,this.transform.position /*new Vector3(0f, 5f, 0f)*/, Quaternion.identity, 0);
                }
                else
                {
                    Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
                }
            }

            player = GameObject.FindGameObjectWithTag("Player");
        }

        void LoadArena()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
            }
            
            Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
            PhotonNetwork.LoadLevel("mazeTestScene" /*  "mazeTestScene"  + PhotonNetwork.CurrentRoom.PlayerCount*/);
        }

        
       
        //public IEnumerator Respawn()
        //{
        //    yield return new WaitForSeconds(0.5f);
        //    player.transform.position = lastCheckpointPos;
        //}

        public void Respawn()
        {
            //yield return new WaitForSeconds(0.5f);
            player.transform.position = lastCheckpointPos;
        }

        public void GameOver()
        {
            //if (photonView.IsMine)
            //{
               Debug.Log("GAME OVER!");
               if (!PhotonNetwork.IsMasterClient)
               {
                   Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
               }
           
               PhotonNetwork.Destroy(player);
                         
               SceneManager.LoadScene("MazeGameScene");
            //}
        }

        public void AddPlayerManagers(PlayerManager p)
        {
            playerManagers.Add(p);
        }

        public void HasFinished()
        {
            foreach (PlayerManager p in playerManagers)
            {
                if(p)
                {
                    p.WinLevel();
                }
            }
            LeaveRoom();
        }
    }

        #endregion
    }


