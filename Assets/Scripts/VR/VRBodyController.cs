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

    private void FixedUpdate()
    {

        float _headRot = PlayerHead.rotation.y;
        float _bodyRot = PlayerBody.rotation.y;
        float _feetRot = PlayerFeet.rotation.y;

        if(Mathf.Abs(_bodyRot - _headRot) > RotationRestraint)
        {

            PlayerBody.rotation = new Quaternion(0, _bodyRot + (_bodyRot - _headRot), 0, PlayerBody.rotation.w);
            
            _bodyRot = PlayerBody.rotation.y;

        }

        if(Mathf.Abs(_feetRot - _bodyRot) > RotationRestraint)
        {

            PlayerFeet.rotation = new Quaternion(0, _feetRot + (_feetRot - _bodyRot), 0, PlayerFeet.rotation.w);

        }

        FeetAnimator.SetFloat("PlayerSpeed", (RB.velocity.magnitude / PlayerMaxSpeed) * AnimationSpeedMult);

        PlayerFeet.position = new Vector3(PlayerHead.position.x, 0, PlayerHead.position.z);

    }

}
