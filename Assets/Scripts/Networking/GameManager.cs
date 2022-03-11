using System;
using System.Collections;
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
        public GameObject playerPrefab;

        #endregion

        [SerializeField] private float levelTransitionDelay = 1f;
        public GameObject player;

        #region Photon Callbacks


        public static GameManager gameManager;


        private void Awake()
        {
            gameManager = this;
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
        }


        #endregion

        #region Private Methods

        void Start()
        {
            Instance = this;

            if (playerPrefab == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
            }
            else
            {
                if (PlayerManager.LocalPlayerInstance == null)
                {
                    Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
                    // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                    PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
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

        public void WinLevel()
        {
            completeLevelUI.SetActive(true);
            //Invoke("LoadNextLevel", levelTransitionDelay);
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
            Debug.Log("GAME OVER!");
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
            }

            if (photonView.IsMine)
            {
               PhotonNetwork.Destroy(player);
           
               SceneManager.LoadScene("waitingRoomScene");

            }
        }
    }

        #endregion
    }


