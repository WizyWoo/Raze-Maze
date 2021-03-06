using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviourPun
{
    PhotonView myPhotonView;

    [SerializeField]
    private float _plMaxSpeed = 10, _camSpeed = 45;
    private PlayerInputActions _playerActions;
    private Rigidbody _rb;
    public Vector2 _moveInput, _camInput;
    [SerializeField] private Camera cam;
    public float minClamp = -45f, maxClamp = 45f, _speed = 10, _speedMultiplier = 1f;
    private float cameraX, timer;

    public GameOverScreenScript gameOver;

    private void OnEnable() {
        _playerActions.Player.Enable();
    }
    private void OnDisable() {
        _playerActions.Player.Disable();
    }
    private void Awake() {
        _playerActions = new PlayerInputActions();
        TryGetComponent<Rigidbody>(out _rb);

        //cam = GetComponentInChildren<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Invoke("CamCorrect",0.4f);
        myPhotonView = GetComponent<PhotonView>();

        if (!myPhotonView.IsMine)
        {
            cam.enabled = false;
        }
    }
    void CamCorrect(){
        cam.transform.localRotation = Quaternion.Euler(0,0,0);
    }
    private void FixedUpdate() {
        /*if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }*/
        // Get control inputs
            _moveInput = _playerActions.Player.Move.ReadValue<Vector2>();
            _camInput = _playerActions.Player.Look.ReadValue<Vector2>();
    
        // Movement
        //transform.Translate(new Vector3(_moveInput.x * _speed * Time.fixedDeltaTime, 0, _moveInput.y * _speed * Time.fixedDeltaTime));
        //_rb.velocity = new Vector3(_moveInput.x * _speed * Time.fixedDeltaTime, 0, _moveInput.y * _speed * Time.fixedDeltaTime);
        _rb.velocity += transform.forward * _moveInput.y * _speed * _speedMultiplier * Time.fixedDeltaTime + transform.right * _moveInput.x * _speed * _speedMultiplier * Time.fixedDeltaTime + transform.up * _rb.velocity.y * Time.fixedDeltaTime;
        
        if(_rb.velocity.magnitude > _plMaxSpeed){
            _rb.velocity = _rb.velocity.normalized * _plMaxSpeed;
        }
        
        // Camera rotating y
        transform.RotateAround(transform.position,Vector3.up,_camInput.x * _camSpeed * Time.fixedDeltaTime);
        
        // Camera clamping
        cameraX = cam.transform.localRotation.x * 100;
        if(cameraX > -minClamp && _camInput.y < 0){
            _camInput = new Vector2(0,0);
        }
        if(cameraX < -maxClamp && _camInput.y > 0){
            _camInput = new Vector2(0,0);
        }
        // Camera rotating x
        cam.transform.localRotation = cam.transform.localRotation * Quaternion.Euler(_camSpeed * Time.fixedDeltaTime * -_camInput.y, 0,0);
        if(Mathf.Abs(transform.rotation.x) > 0 || Mathf.Abs(transform.rotation.z) > 0){
            //transform.Rotate(new Vector3(-transform.rotation.x, 0, -transform.rotation.z));
            transform.localRotation = Quaternion.Euler(0,transform.localRotation.y, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        

        
    }

    
    
}
