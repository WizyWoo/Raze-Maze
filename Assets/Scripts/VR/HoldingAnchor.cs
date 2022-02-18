using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingAnchor : MonoBehaviour
{

    //How to check if it is already held when another hand/player tries to pick it up
    //How to make the rotation happen nicely, and what happens when there's only 1 anchor

    [Tooltip("If this is not the main anchor, do not link an anchor here")]
    public Transform LinkedAnchorTransform;
    [Tooltip("The object will rotate from this anchor if it is linked to another anchor"),SerializeField]
    private bool mainAnchor;
    private HoldingAnchor linkedAnchor;
    private HoldingAnchorActivatable linkedActivatableAnchor;
    private bool isHeld;



}
