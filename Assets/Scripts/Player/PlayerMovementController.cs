using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{

    public float MovementSpeed, SprintSpeedModifier, JumpHeight, AirControlMult, CoyoteTime;
    public bool DrawGizmos;
    private float sprinting, coyoteTimer;
    [SerializeField]
    public bool grounded;
    private Vector3 movementDir, spawnOrigin;
    private Rigidbody rb;
    private LayerMask playerMask;
    private CameraController cc;
    [SerializeField]
    private Vector3 groundCheckV3, groundCheckDirV3;
    [SerializeField]
    private float groundCheckDist;

    void Start()
    {

        cc = Camera.main.GetComponent<CameraController>();
        rb = gameObject.GetComponent<Rigidbody>();
        playerMask = 1 << LayerMask.NameToLayer("Player");
        spawnOrigin = transform.position;

    }

    void Update()
    {

        if(transform.position.y < -5)
        {

            rb.velocity = Vector3.zero;
            transform.position = spawnOrigin;

        }

        if(Physics.SphereCast(transform.position + groundCheckV3, groundCheckDist, groundCheckDirV3, out RaycastHit groundCheck, 1f, ~playerMask, QueryTriggerInteraction.Ignore))
        {

            grounded = true;
            coyoteTimer = CoyoteTime;

        }
        else if(coyoteTimer <= 0)
        {

            grounded = false;

        }

        if(Input.GetKeyDown(KeyCode.Space) && grounded == true)
        {

            rb.velocity = new Vector3(rb.velocity.x, JumpHeight, rb.velocity.z);
            coyoteTimer = 0;

        }

        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {

            sprinting = SprintSpeedModifier;
            cc.Sprinting = true;

        }
        else
        {

            sprinting = 1;
            cc.Sprinting = false;

        }
        
        movementDir = (transform.forward * sprinting * Input.GetAxis("Vertical") * MovementSpeed) + (transform.right * sprinting * Input.GetAxis("Horizontal") * MovementSpeed);

        if(grounded)
        {

            movementDir += new Vector3(0, rb.velocity.y, 0);

        }
        else
        {

            Vector3 airControlV3 = new Vector3();

            if(Mathf.Abs(rb.velocity.x) < MovementSpeed * sprinting)
            {

                airControlV3.x = (movementDir.x * AirControlMult) * Time.deltaTime + rb.velocity.x;

            }
            else
            {

                airControlV3.x = rb.velocity.x;

            }

            if(Mathf.Abs(rb.velocity.z) < MovementSpeed * sprinting)
            {

                airControlV3.z = (movementDir.z * AirControlMult) * Time.deltaTime + rb.velocity.z;

            }
            else
            {

                airControlV3.z = rb.velocity.z;

            }

            airControlV3.y = rb.velocity.y;
            movementDir = airControlV3;

        }

        rb.velocity = movementDir;

        if(coyoteTimer > 0)
            coyoteTimer -= Time.deltaTime;

    }

    private void OnDrawGizmos()
    {

        if(DrawGizmos)
        {

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position + groundCheckV3, groundCheckDist);

        }

    }

}
