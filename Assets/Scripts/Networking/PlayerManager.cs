using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using System.Collections;
using Photon.Pun.Demo.PunBasics;

namespace Com.MyCompany.MyGame
{
    /// <summary>
    /// Player manager.
    /// Handles fire Input and Beams.
    /// </summary>
    public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
    {
        #region IPunObservable implementation

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                // We own this player: send the others our data
                //stream.SendNext(IsFiring);
                stream.SendNext(Health);
            }
            else
            {
                // Network player, receive data
               // this.IsFiring = (bool)stream.ReceiveNext();
                this.Health = (float)stream.ReceiveNext();
            }
        }

        #endregion

        #region Public Fields

        [Tooltip("The Player's UI GameObject Prefab")]
        [SerializeField]
        public GameObject PlayerUiPrefab;

        [Tooltip("The current Health of our player")]
        public float Health = 20f;

        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;

        public int lives = 3;
        public GameOverScreenScript gameOver;

        #endregion

        #region Private Fields

        ////[Tooltip("The Beams GameObject to control")]
        //[SerializeField] private GameObject mine;
        ////True, when the user is firing
        //bool IsFiring;
        #endregion

        #region Private Methods

#if UNITY_5_4_OR_NEWER
        void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode loadingMode)
            {
                this.CalledOnLevelWasLoaded(scene.buildIndex);
            }
        #endif

        #endregion

        #region MonoBehaviour CallBacks

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
        /// </summary>
        void Awake()
        {
            // #Important
            // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
            if (photonView.IsMine)
            {
                PlayerManager.LocalPlayerInstance = this.gameObject;
            }
            // #Critical
            // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
            DontDestroyOnLoad(this.gameObject);

            //if (beams == null)
            //{
            //    Debug.LogError("<Color=Red><a>Missing</a></Color> Beams Reference.", this);
            //}
            //else
            //{
            //    beams.SetActive(false);
            //}
        }

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during initialization phase.
        /// </summary>
        void Start()
        {
            #if UNITY_5_4_OR_NEWER
                // Unity 5.4 has a new scene management. register a method to call CalledOnLevelWasLoaded.
                UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
            #endif

            if (PlayerUiPrefab != null)
            {
                GameObject _uiGo = Instantiate(PlayerUiPrefab);
                _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
            }
            else
            {
                Debug.LogWarning("<Color=Red><a>Missing</a></Color> PlayerUiPrefab reference on player Prefab.", this);
            }

            //CameraWork _cameraWork = this.gameObject.GetComponentInChildren<CameraWork>();


            //if (_cameraWork != null)
            //{
            //    if (photonView.IsMine)
            //    {
            //        _cameraWork.OnStartFollowing();
            //    }
            //}
            //else
            //{
            //    Debug.LogError("<Color=Red><a>Missing</a></Color> CameraWork Component on playerPrefab.", this);
            //}
        }

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity on every frame.
        /// </summary>
        void Update()
        {

            if (photonView.IsMine)
            {
                ProcessInputs();
            }

            if (photonView.IsMine)
            {
                ProcessInputs();
                //if (Health <= 0f)
                //{
                //    GameManager.Instance.LeaveRoom();
                //}
            }

            // trigger Beams active state
            //if (beams != null && IsFiring != beams.activeInHierarchy)
            //{
            //    beams.SetActive(IsFiring);
            //}

            //Damage();
        }

        #if UNITY_5_4_OR_NEWER
        public override void OnDisable()
        {
            // Always call the base to remove callbacks
            base.OnDisable();
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        #endif

        /// <summary>
        /// MonoBehaviour method called when the Collider 'other' enters the trigger.
        /// Affect Health of the Player if the collider is a beam
        /// Note: when jumping and firing at the same, you'll find that the player's own beam intersects with itself
        /// One could move the collider further away to prevent this or check if the beam belongs to the player.
        /// </summary>
        void OnTriggerEnter(Collider other)
        {
            if (!photonView.IsMine)
            {
                return;
            }
            // We are only interested in Beamers
            // we should be using tags but for the sake of distribution, let's simply check by name.
            if (!other.tag.Contains("Weapon") && !other.tag.Contains("Trap"))
            {
                return;
            }

            if(other.tag.Contains("Weapon"))
                Health -= other.GetComponent<WeaponController>().Damage;

            if (other.tag.Contains("Trap"))
                Health -= other.GetComponent<TrapController>().Damage;

        }

        /// <summary>
        /// MonoBehaviour method called once per frame for every Collider 'other' that is touching the trigger.
        /// We're going to affect health while the beams are touching the player
        /// </summary>
        /// <param name="other">Other.</param>
        void OnTriggerStay(Collider other)
        {
            // we dont' do anything if we are not the local player.
            //if (!photonView.IsMine)
            //{
            //    return;
            //}
            //// We are only interested in Beamers
            //// we should be using tags but for the sake of distribution, let's simply check by name.
            //if (!other.tag.Contains("Weapon") && !other.tag.Contains("Trap"))
            //{
            //    return;
            //}
            //// we slowly affect health when beam is constantly hitting us, so player has to move to prevent death.
            //Health -= 0.1f * Time.deltaTime;
        }

        #if !UNITY_5_4_OR_NEWER
        /// <summary>See CalledOnLevelWasLoaded. Outdated in Unity 5.4.</summary>
        void OnLevelWasLoaded(int level)
        {
            this.CalledOnLevelWasLoaded(level);
        }
        #endif


        void CalledOnLevelWasLoaded(int level)
        {
            // check if we are outside the Arena and if it's the case, spawn around the center of the arena in a safe zone
            //if (!Physics.Raycast(transform.position, -Vector3.up, 5f))
            //{
            //    transform.position = new Vector3(0f, 5f, 0f);
            //}

            GameObject _uiGo = Instantiate(this.PlayerUiPrefab);
            _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        }

        #endregion

        #region Custom

        /// <summary>
        /// Processes the inputs. Maintain a flag representing when the user is pressing Fire.
        /// </summary>
        void ProcessInputs()
        {
            //if (Input.GetButtonDown("Fire1"))
            //{
            //    if (!IsFiring)
            //    {
            //        IsFiring = true;
            //    }
            //}
            //if (Input.GetButtonUp("Fire1"))
            //{
            //    if (IsFiring)
            //    {
            //        IsFiring = false;
            //    }
            //}
        }

        #endregion

        public void Damage()
        {
            Health--;

            if (Health <= 0)
            {
                lives--;
                GameManager.gameManager.Respawn();

                if (lives <= 0)
                    gameOver.SetUp();
            }
        }
    }
}
