using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VrPlayerController : MonoBehaviour
{

    public VRPlayerInputs PlayerInputs;
    public float MovementSpeed, TurnSpeed;
    public Transform MoveRelativeTo, RotateAround, CameraTransform;
    private InputAction move, look;
    private Rigidbody rb;
    public Vector2 tempV2;

    private void Awake()
    {

        PlayerInputs = new VRPlayerInputs();
        rb = gameObject.GetComponent<Rigidbody>();

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

        RotateAround.RotateAround(CameraTransform.position, Vector3.up, TurnSpeed * Time.deltaTime * look.ReadValue<Vector2>().x);

    }

    public void OnRespawning()
    {

        move.Disable();

    }

    public void OnRespawned()
    {

        move.Enable();

    }

}
