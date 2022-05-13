using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VrPlayerController : MonoBehaviour
{

    public VRPlayerInputs PlayerInputs;
    public bool SnapTurning, MovementVignette;
    public float MovementSpeed, TurnSpeed;
    public Transform MoveRelativeTo, RotateAround, CameraTransform;
    public VrHandsController[] Hands;
    private InputAction move, look;
    private Rigidbody rb;
    private Vector2 tempV2;
    private LocalGameController gc;

    private void Awake()
    {

        PlayerInputs = new VRPlayerInputs();
        rb = gameObject.GetComponent<Rigidbody>();

        gc = LocalGameController.main;

        SnapTurning = gc.SnapTurning;
        MovementVignette = gc.MovementVignette;


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

        tempV2 = move.ReadValue<Vector2>();

        Vector3 forwardDir = new Vector3(MoveRelativeTo.forward.x, 0, MoveRelativeTo.forward.z).normalized;
        Vector3 rightDir = new Vector3(MoveRelativeTo.right.x, 0, MoveRelativeTo.right.z).normalized;
        Vector3 moveDir = ((new Vector3(forwardDir.x, 0, forwardDir.z) * tempV2.y) + (new Vector3(rightDir.x, 0, rightDir.z) * tempV2.x)) * MovementSpeed;

        rb.velocity = new Vector3(moveDir.x, rb.velocity.y, moveDir.z);

        if(!SnapTurning)
        {

            RotateAround.RotateAround(CameraTransform.position, Vector3.up, TurnSpeed * Time.deltaTime * look.ReadValue<Vector2>().x);

        }
        else
        {

            Debug.Log("Nope not done");

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
