using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using System.Collections;
using Photon.Pun.Demo.PunBasics;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace Com.MyCompany.MyGame
{
    /// <summary>
    /// Player manager.
    /// Handles fire Input and Beams.
    /// </summary>
    public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable, IHit
    {
        #region Public Fields

        [Tooltip("The Player's UI GameObject Prefab")]
        [SerializeField]
        public GameObject PlayerUiPrefab;

        [Tooltip("The current Health of our player")]
        public float Health;

        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;

        public int lives = 3;
        public GameOverScreenScript gameOver;

        public bool readied = false, DamageLocked, someoneHasWon = false;

        public static PlayerManager playerManager;

        public Material HitMat;

        private float takingDamageCounter;

        private Vignette vignette;

        public Volume volume;

        [SerializeField] GameObject panel, timerObj;
        [SerializeField] Image timeImage;
        [SerializeField] Text timeText;
        [SerializeField] float duration, currentTime;

        //postProcessing info
        // public float intensity = 0.75f, duration = 0.5f;
        // public Volume volume = null;
        // private Vignette vignette = null;

        private VrPlayerController vrController;

        #endregion

        #region Private Fields

        ////[Tooltip("The Beams GameObject to control")]
        //[SerializeField] private GameObject mine;
        ////True, when the user is firing
        //bool IsFiring;
        #endregion

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
                playerManager = this;
            }
            // #Critical
            // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
            DontDestroyOnLoad(this.gameObject);

            gameObject.name = photonView.Owner.NickName;
          

            //getting the vignette
            // if(volume.profile.TryGet(out Vignette vignette))
            //     this.vignette = vignette;

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

            //postFX = gameObject.GetComponentInChildren<Volume>().profile;
            //volume = LocalGameController.main.GetComponentInChildren<Volume>();

            GameManager.gameManager.AddPlayerManagers(this);

            volume.profile.TryGet(out vignette);

            Invoke("SetInitialPos", 0.5f);
            //CameraWork _cameraWork = this.gameObject.GetComponentInChildren<CameraWork>();
            vrController =  gameObject.GetComponent<VrPlayerController>();

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

             originalColor = ren.material;
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

            if(takingDamageCounter > 0)
            {
                ren.material = HitMat;
                volume.weight = takingDamageCounter;

                takingDamageCounter -= Time.deltaTime;
            } else
            {
                ren.material = originalColor;
                volume.weight = 0;
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
        //void OnTriggerEnter(Collider other)
        //{
        //    if (!photonView.IsMine)
        //    {
        //        return;
        //    }
        //    // We are only interested in Beamers
        //    // we should be using tags but for the sake of distribution, let's simply check by name.
        //    if (!other.tag.Contains("Weapon") && !other.tag.Contains("Trap"))
        //    {
        //        return;
        //    }

        //    //if(other.tag.Contains("Weapon"))
        //    //    Health -= other.GetComponent<WeaponController>().Damage;

        //    //if (other.tag.Contains("Trap"))
        //    //     Damage(trapDamage);

        //}

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

        void SetInitialPos()
        {
            if(photonView.IsMine)
                GameManager.gameManager.lastCheckpointPos = transform.position;
        }


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

        private float redFlashTime = 1f;
        private Material originalColor;
        public Renderer ren;

        public void Heal()
        {

            Health = 1;
            lives = 3;

        }

        public void Damage(float damage = 0)
        {
            if(!DamageLocked)
            {
                takingDamageCounter = 0.8f;
                Health -= damage;
                //FadeIn();

                if (Health <= 0)
                {
                    lives--;
                    Health = 1f;

                    //StartCoroutine(GameManager.gameManager.Respawn());
                    if (photonView.IsMine)
                    {                                  
                        GameManager.gameManager.Invoke("Respawn", 3f);
                        GetComponentInChildren<PlayerItemController>().DropItem(0);

                        DamageLocked = true;

                        Invoke("UnlockDamage", 4f);

                        if (lives <= 0)
                            GameManager.gameManager.Invoke("GameOver", 2f);
                        //GameManager.gameManager.LeaveRoom();

                        vrController.OnRespawning();
                    }
                }
                Debug.Log(gameObject.name + " was damaged for " + damage);

                // FadeOut();
            }
        }

        //vignette fade in
        // public void FadeIn()
        // {
        //     StartCoroutine(Fade(0, intensity));
        // }

        // //vignette fade out
        // public void FadeOut()
        // {
        //     StartCoroutine(Fade(intensity, 0));
        // }

        // //vignette Fade
        // private IEnumerator Fade(float startValue, float endValue)
        // {
        //     float elapsedTime = 0f;

        //     while(elapsedTime <= duration)
        //     {
        //         //blend value 
        //         float blend = elapsedTime / duration;
        //         elapsedTime += Time.deltaTime;

        //         //apply intensity
        //         float intensity = Mathf.Lerp(startValue, endValue, blend);
        //         ApplyValue(intensity);

        //         yield return null;
        //     }
        // }

        // //apply vignette intensity
        // private void ApplyValue(float value)
        // {
        //     vignette.intensity.Override(value);
        // }

         public void WinLevel()
        {
            if(!photonView.IsMine)
            {
                photonView.RPC("SomeoneWon", RpcTarget.All);
                //GameManager.gameManager.LeaveRoom();
                //Invoke("LoadNextLevel", levelTransitionDelay);       
            }
        }

        [PunRPC]
        private void SomeoneWon()
        {
            if(photonView.IsMine)
            {
                Debug.Log(gameObject.name);
                timerObj.SetActive(true);
                currentTime = duration;
                timeText.text = currentTime.ToString();
                StopAllCoroutines();
                StartCoroutine(CountdownTimer());
            }   
        }

        public void UnlockDamage()
        {
            DamageLocked = false;
            vrController.OnRespawned();
        }

    public IEnumerator CountdownTimer()
    {
        while(currentTime >= 0)
        {
            timeImage.fillAmount = Mathf.InverseLerp(0, duration, currentTime);
            timeText.text = currentTime.ToString();
            yield return new WaitForSeconds(1f);
            currentTime--;
        }
        OpenPanel();

        yield return new WaitForSeconds(1.5f);
        panel.SetActive(false);
        GameManager.gameManager.LeaveRoom();
    }

    void OpenPanel()
    {
        timerObj.SetActive(false);
        timeText.text = "";
        panel.SetActive(true);
    }

    }
}
