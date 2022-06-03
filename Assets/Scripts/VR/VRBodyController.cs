using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRBodyController : MonoBehaviour
{

    public Transform PlayerHead, PlayerBody, PlayerFeet;
    public float RotationRestraint, PlayerMaxSpeed;
    public Rigidbody RB;
    public Vector2 TestVector;
    public float AnimationSpeedMult;
    public Animator FeetAnimator;

    private void Update()
    {

        //FeetAnimator.SetFloat("PlayerSpeed", RB.velocity.magnitude / PlayerMaxSpeed);

        Vector2 _feetForward = new Vector2(PlayerFeet.forward.x, PlayerFeet.forward.z).normalized;
        Vector2 _velocityForward = new Vector2(RB.velocity.x, RB.velocity.z).normalized;

        PlayerFeet.rotation = new Quaternion(0, PlayerFeet.rotation.y + Dot(_feetForward, _velocityForward), 0, PlayerFeet.rotation.w);

    }

    public float Dot(Vector2 _a, Vector2 _b)
    {

        float _returnVal;

        _returnVal = (_a.x * _b.x) + (_a.y * _b.y);

        return _returnVal;

    }

}
