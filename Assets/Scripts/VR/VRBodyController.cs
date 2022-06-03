using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRBodyController : MonoBehaviour
{

    public Transform PlayerHead, PlayerBody, PlayerFeet;
    public float RotationRestraint, PlayerMaxSpeed;
    public Rigidbody RB;
    public float AnimationSpeedMult;
    public Animator FeetAnimator;

    private void Update()
    {

        //FeetAnimator.SetFloat("PlayerSpeed", RB.velocity.magnitude / PlayerMaxSpeed);

        Vector3 _velocityForward = new Vector3(RB.velocity.x, 0, RB.velocity.z).normalized;

        PlayerFeet.LookAt(_velocityForward + PlayerFeet.position, Vector3.up);

    }

}
