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
    public float Head, Body, Feet;

    private void Update()
    {

        Debug.Log("Hello I do work");

        Head = PlayerHead.rotation.y;
        Body = PlayerBody.rotation.y;
        Feet = PlayerFeet.rotation.y;

        if(Mathf.Abs(Body - Head) > RotationRestraint)
        {

            PlayerBody.rotation = new Quaternion(0, PlayerHead.rotation.y, 0, PlayerBody.rotation.w);
            
            Body = PlayerBody.rotation.y;
            
            Debug.Log("Rotating body");

        }

        if(Mathf.Abs(Feet - Body) > RotationRestraint)
        {

            PlayerFeet.rotation = new Quaternion(0, (Feet + (Feet - Body)) * 360, 0, PlayerFeet.rotation.w);

        }

        FeetAnimator.SetFloat("PlayerSpeed", (RB.velocity.magnitude / PlayerMaxSpeed) * AnimationSpeedMult);

        PlayerFeet.position = new Vector3(PlayerHead.position.x, 0, PlayerHead.position.z);

    }

}
