using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10, _camSpeed = 45;
    private PlayerInputActions _playerActions;
    private Rigidbody _rb;
    private Vector2 _moveInput, _camInput;
    private Camera cam;
    public float minClamp = -45f, maxClamp = 45f;
    private float cameraX;
    private void OnEnable() {
        _playerActions.Player.Enable();
    }
    private void OnDisable() {
        _playerActions.Player.Disable();
    }
    private void Awake() {
        _playerActions = new PlayerInputActions();
        TryGetComponent<Rigidbody>(out _rb);

        cam = GetComponentInChildren<Camera>();
        Cursor.visible = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }
    private void FixedUpdate() {
        // Get control inputs
        _moveInput = _playerActions.Player.Move.ReadValue<Vector2>();
        _camInput = _playerActions.Player.Look.ReadValue<Vector2>();
    
        // Movement
        _rb.AddForce(transform.forward * _moveInput.y * _speed, ForceMode.Force);
        _rb.AddForce(transform.right * _moveInput.x * _speed, ForceMode.Force);
        
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
