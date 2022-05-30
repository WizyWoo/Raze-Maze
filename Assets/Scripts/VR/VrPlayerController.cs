using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VrPlayerController : MonoBehaviour
{

    public VRPlayerInputs PlayerInputs;
    public Volume MovementVignetteVolume;
    public RazeMazeSettings Vals;
    public float MovementSpeed, SnapTurnDeadzone;
    public Transform MoveRelativeTo, RotateAround, CameraTransform;
    public VrHandsController[] Hands;
    private Vignette movementVignette;
    private InputAction move, look;
    private Rigidbody rb;
    private Vector2 moveV2, lookV2;
    private LocalGameController gc;
    private bool snapRotLock;

    private void Awake()
    {

        PlayerInputs = new VRPlayerInputs();
        rb = gameObject.GetComponent<Rigidbody>();

    }

    private void Start()
    {

        gc = LocalGameController.main;
        gc.PlayerController = this;
        Vals = gc.LoadSettings();

        MovementVignetteVolume.profile.TryGet<Vignette>(out movementVignette);

    }

    private void OnEnable()
    {

        move = PlayerInputs.Player.Move;
        look = PlayerInputs.Player.Look;
        move.Enable();
        look.Enable();

    }

    private void OnDisable()
    {

        move.Disable();
        look.Disable();

    }

    private void Update()
    {

        moveV2 = move.ReadValue<Vector2>();
        lookV2 = look.ReadValue<Vector2>();

        Vector3 forwardDir = new Vector3(MoveRelativeTo.forward.x, 0, MoveRelativeTo.forward.z).normalized;
        Vector3 rightDir = new Vector3(MoveRelativeTo.right.x, 0, MoveRelativeTo.right.z).normalized;
        Vector3 moveDir = ((new Vector3(forwardDir.x, 0, forwardDir.z) * moveV2.y) + (new Vector3(rightDir.x, 0, rightDir.z) * moveV2.x)) * MovementSpeed;

        float _vignetteStrenght = moveV2.magnitude;

        rb.velocity = new Vector3(moveDir.x, rb.velocity.y, moveDir.z);

        if(!Vals.SnapTurning)
        {

            RotateAround.RotateAround(CameraTransform.position, Vector3.up, Vals.RotationSpeed * Time.deltaTime * lookV2.x);
            if(Mathf.Abs(lookV2.x) > _vignetteStrenght)
            {

                _vignetteStrenght = Mathf.Abs(lookV2.x);

            }

        }
        else if(!snapRotLock && Mathf.Abs(lookV2.x) > SnapTurnDeadzone)
        {

            if(lookV2.x > 0)
            {

                RotateAround.RotateAround(CameraTransform.position, Vector3.up, Vals.DegreesPerRotate);
                snapRotLock = true;
                _vignetteStrenght = 1;

            }
            else if(lookV2.x < 0)
            {

                RotateAround.RotateAround(CameraTransform.position, Vector3.up, -Vals.DegreesPerRotate);
                snapRotLock = true;
                _vignetteStrenght = 1;

            }

        }
        if(snapRotLock && Mathf.Abs(lookV2.x) <= SnapTurnDeadzone)
        {

            snapRotLock = false;

        }

        if(Vals.MovementVignette)
        {

            movementVignette.intensity.Override(_vignetteStrenght);

        }

    }

    public void OnRespawning()
    {

        move.Disable();
        Hands[0].SetInteractionState(false);
        Hands[1].SetInteractionState(false);

    }

    public void OnRespawned()
    {

        move.Enable();
        Hands[0].SetInteractionState(true);
        Hands[1].SetInteractionState(true);

    }

}
