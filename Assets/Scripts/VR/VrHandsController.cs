using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRHandsController : MonoBehaviour
{

    /*
    When I click the grab button I want to check for any HoldingAnchors in a certain distance from the hand, if there are any I want them to be put in my hand as long as I keep my hand closed
    When I click the trigger while I have a holdable object in my hand check for if it is an activatable then it should activate the Fireweapon script in the weaponcontroller
    */

    public enum Hand
    {

        Right,
        Left

    }

    public GameObject RightHand, LeftHand;
    public Transform WeaponDisplay, TrapDisplay;
    private HoldingAnchor holding;
    
}