using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    //This script controls the players camera and their Field of view when running.

    public bool Sprinting;
    public float InputSensitivity, FovChangeSpeed;
    [SerializeField]
    private Transform cameraPivot;
    private float xRot, fov, sprint;
    private Transform player;
    private PlayerMovementController pMC;
    private Rigidbody rB;

    void Start()
    {

        fov = Camera.main.fieldOfView;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        player = transform.root;
        pMC = player.GetComponent<PlayerMovementController>();
        rB = player.GetComponent<Rigidbody>();

    }

    void Update()
    {

        player.Rotate(Vector3.up, Input.GetAxisRaw("Mouse X") * InputSensitivity);
        xRot -= Input.GetAxisRaw("Mouse Y") * InputSensitivity;
        xRot = Mathf.Clamp(xRot, -90, 90);
        cameraPivot.localEulerAngles = new Vector3(xRot, 0, 0);

        if(Sprinting && sprint != 1)
        {

            sprint = Mathf.Clamp(sprint + (Time.deltaTime * FovChangeSpeed), 0, 1);
            Camera.main.fieldOfView = fov + (10 * sprint);

        }
        else if(!Sprinting && rB.velocity.magnitude < pMC.MovementSpeed * 1.2f)
        {
            
            sprint = Mathf.Clamp(sprint - (Time.deltaTime * FovChangeSpeed), 0, 1);
            Camera.main.fieldOfView = fov + (10 * sprint);

        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {

            if(Cursor.lockState == CursorLockMode.Locked)
            {

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

            }
            else
            {

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

            }

        }

    }

}
