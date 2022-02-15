using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerWeaponController : MonoBehaviourPunCallbacks
{
    
    /*

    TODO
    - Align placement with surface
    - Exessive placement checks for checking all directions to keep traps out of walls
    - Make this use the ItemManager instead of own enums and values
    - Make this independent on Refs to ui

    */

    public enum ScalingMode
    {

        None,
        Forward,
        Sideways,
        SurfaceAlign

    }

    public enum WeaponUseMode
    {

        Melee,
        Throw,
        Ranged

    }

    [Tooltip("This might be a bit heavy")]
    public bool ExstensivePlacementChecks, UseUIFeedback;
    public GameObject[] TrapPrefabs, WeaponPrefabs;
    public ScalingMode[] TrapScaleMode;
    public float[] TrapActivationDelay;
    public WeaponUseMode[] AttackMode;
    public float TrapPlaceDistance, PlacementCheckRange, ScaleMaxLenght, ScaleMinLenght, MeleeReach, ThrowVelocity, ShotRange;
    public int EquippedWeaponID {get; private set;}
    public Text WeaponIDText, FeedbackText;
    public Transform Parent;
    [SerializeField]
    private GameObject droppedWeaponPrefab, weaponInHand;
    [SerializeField]
    private Transform handTransform;
    private bool placingTrap, validLocation;
    private float timer;
    private (Vector3, Vector3, Quaternion) placementInfo;
    private Transform ghostTrap;
    private Vector3 trapBounds, boundsOffset;
    private LayerMask mask, playerMask;
    [Tooltip("Should be the name of the folder in Resources"), SerializeField]
    private string weaponFolderName, trapFolderName, pickupFolderName;

    private void Start()
    {

        mask = ~((1 << LayerMask.NameToLayer("Player")) + (1 << LayerMask.NameToLayer("Traps")));
        playerMask = 1 << LayerMask.NameToLayer("Player");

        UpdateUIRefs();

    }

    public void UpdateUIRefs()
    {

        if(UseUIFeedback)
        {

            GameObject temp = GameObject.FindGameObjectWithTag("Hud");
            Text[] tempTexts = temp.GetComponentsInChildren<Text>();

            for(int i = 0; i < tempTexts.Length; i++)
            {

                if(tempTexts[i].name == "PlacementFeedback")
                {

                    FeedbackText = tempTexts[i];

                }
                else if(tempTexts[i].name == "WeaponID")
                {

                    WeaponIDText = tempTexts[i];

                }

            }

            FeedbackText.text = "";

        }

    }

    public void PickedUpWeapon(int weaponID)
    {

        if(EquippedWeaponID != 0)
        {

            if(weaponInHand)
                PhotonNetwork.Destroy(weaponInHand);
            GameObject temp = PhotonNetwork.Instantiate(pickupFolderName + droppedWeaponPrefab.name, transform.position, Quaternion.identity);
            temp.GetComponent<WeaponPickup>().WeaponID = EquippedWeaponID;
            temp.name = "Dropped Weapon ^ " + EquippedWeaponID;

        }
        
        placingTrap = false;
        UpdateEquippedWeapon(weaponID);

    }

    private void UpdateEquippedWeapon(int iD)
    {

        EquippedWeaponID = iD;

        if(UseUIFeedback)
        {

            WeaponIDText.text = "^ Weapon ID: " + EquippedWeaponID + " ^";

        }

    }

    #region Placement Modes

    private (Vector3 position, Vector3 scale, Quaternion rotation) AlignToSurface(Vector3 tempPos)
    {

        return (Vector3.zero, Vector3.one, Quaternion.identity);

    }

    private (Vector3 position, Vector3 scale, Quaternion rotation) ScaleSideways(Vector3 tempPos)
    {

        Vector3 _position, _scale;
        Quaternion _rotation;

        RaycastHit hit;
        if(Physics.Raycast(tempPos, Vector3.down, out hit, 1, mask, QueryTriggerInteraction.Ignore))
        {

            _position = hit.point + (Vector3.up * (trapBounds.y / boundsOffset.y));
            validLocation = true;

            float rightDist = 0, leftDist = 0;

            if(Physics.Raycast(_position, ghostTrap.right, out hit, ScaleMaxLenght/2, mask, QueryTriggerInteraction.Ignore))
            {

                rightDist = Vector3.Distance(_position, hit.point);

            }

            if(Physics.Raycast(_position, ghostTrap.right * -1, out hit, ScaleMaxLenght/2, mask, QueryTriggerInteraction.Ignore))
            {

                leftDist = Vector3.Distance(_position, hit.point);

            }

            if(rightDist != 0 || leftDist != 0)
            {

                if((rightDist < leftDist || leftDist < ScaleMinLenght) && rightDist > ScaleMinLenght)
                {

                    _scale = new Vector3(2 * rightDist, 1, 1);

                }
                else if((leftDist < rightDist || rightDist < ScaleMinLenght) && leftDist > ScaleMinLenght)
                {

                    _scale = new Vector3(2 * leftDist, 1, 1);

                }
                else
                {

                    //invalid?
                    _scale = Vector3.one;

                }

            }
            else
            {
                
                _scale = new Vector3(1 * ScaleMaxLenght, 1, 1);

            }

        }
        else
        {

            _position = tempPos + (Vector3.up * (trapBounds.y - boundsOffset.y));
            _scale = Vector3.one;
            validLocation = false;

        }

        _rotation = Parent.rotation;

        return (_position, _scale, _rotation);

    }

    private (Vector3 position, Vector3 scale, Quaternion rotation) ScaleForward(Vector3 tempPos)
    {

        Vector3 _position, _scale;
        Quaternion _rotation;

        RaycastHit hit;
        if(Physics.Raycast(tempPos, Vector3.down, out hit, 1, mask, QueryTriggerInteraction.Ignore))
        {

            _position = hit.point + (Vector3.up * (trapBounds.y - boundsOffset.y));
            validLocation = true;

            if(Physics.Raycast(_position, ghostTrap.forward, out hit, ScaleMaxLenght / 2, mask, QueryTriggerInteraction.Ignore))
            {

                _scale = new Vector3(1, 1, 1 * Mathf.Clamp(Vector3.Distance(_position, hit.point) * 2, ScaleMinLenght * 2, ScaleMaxLenght));

            }
            else
            {

                _scale = new Vector3(1, 1, 1 * ScaleMaxLenght);

            }

        }
        else
        {

            _position = tempPos + (Vector3.up * (trapBounds.y - boundsOffset.y));
            _scale = Vector3.one;
            validLocation = false;

        }

        _rotation = Parent.rotation;

        return (_position, _scale, _rotation);

    }

    private (Vector3 position, Vector3 scale, Quaternion rotation) NoScaling(Vector3 tempPos)
    {

        Vector3 _position;
        Quaternion _rotation;

        RaycastHit hit;
        if(Physics.Raycast(tempPos, Vector3.down, out hit, 1, mask, QueryTriggerInteraction.Ignore))
        {

            _position = hit.point + (Vector3.up * (trapBounds.y - boundsOffset.y));
            validLocation = true;

        }
        else
        {

            _position = tempPos + (Vector3.up * (trapBounds.y - boundsOffset.y));
            validLocation = false;

        }

        //might wanna change this later, maybe?
        _rotation = Parent.rotation;

        return (_position, Vector3.zero, _rotation);

    }

    #endregion

    private void EquipWeapon()
    {

        //Desktop

        if(weaponInHand == null)
        {

            weaponInHand = PhotonNetwork.Instantiate(weaponFolderName + WeaponPrefabs[EquippedWeaponID].name, handTransform.position, handTransform.rotation);

            if(weaponInHand.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {

                rb.isKinematic = true;

            }

        }
        else
        {

            PickedUpWeapon(0);

        }

    }

    private void ThrowWeapon()
    {

        //Desktop

        GameObject temp = PhotonNetwork.Instantiate(weaponFolderName + WeaponPrefabs[EquippedWeaponID].name, transform.position, transform.rotation);
        WeaponController controller = temp.GetComponent<WeaponController>();
        controller.WeaponID = EquippedWeaponID;
        controller.Thrown();
        temp.GetComponent<Rigidbody>().velocity = transform.forward * ThrowVelocity;

        PhotonNetwork.Destroy(weaponInHand);

        UpdateEquippedWeapon(0);

    }

    private void FireWeapon()
    {

        //VR and Desktop
        weaponInHand.GetComponent<WeaponController>().FireWeapon();
        weaponInHand.GetComponent<WeaponController>().Firing = true;

    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.Mouse1) && EquippedWeaponID != 0 && weaponInHand == null)
        {

            string tempFeedback = "";

            if(!placingTrap)
            {

                placingTrap = true;
                ghostTrap = Instantiate(TrapPrefabs[EquippedWeaponID]).transform;
                trapBounds = ghostTrap.GetComponent<BoxCollider>().bounds.extents;
                boundsOffset = ghostTrap.GetComponent<BoxCollider>().bounds.center;

            }
            else if(validLocation)
            {

                Transform temp = PhotonNetwork.Instantiate(trapFolderName + TrapPrefabs[EquippedWeaponID].name, placementInfo.Item1, placementInfo.Item3).transform;
                if(TrapScaleMode[EquippedWeaponID] != ScalingMode.None)
                    temp.localScale = placementInfo.Item2;
                temp.gameObject.GetComponent<TrapController>().TrapPlaced(TrapActivationDelay[EquippedWeaponID], EquippedWeaponID);
                Destroy(ghostTrap.gameObject);
                UpdateEquippedWeapon(0);
                tempFeedback = "";

            }
            else
            {

                tempFeedback = "Placement is not valid";
                timer = 2;

            }

            if(UseUIFeedback)
            {

                FeedbackText.text = tempFeedback;

            }

        }
        else if(Input.GetKeyDown(KeyCode.Mouse0) && EquippedWeaponID != 0 && weaponInHand)
        {

            switch(AttackMode[EquippedWeaponID])
            {

                case WeaponUseMode.Melee:
                //MeleeAttack();
                break;

                case WeaponUseMode.Throw:
                ThrowWeapon();
                break;

                case WeaponUseMode.Ranged:
                FireWeapon();
                break;

            }

        }

        if(Input.GetKeyDown(KeyCode.R) && EquippedWeaponID != 0 && !placingTrap)
        {

            EquipWeapon();

        }

        if(UseUIFeedback)
        {

            if(timer > 0)
            {

                timer -= Time.deltaTime;

            }
            else if(FeedbackText.text != "")
            {

                FeedbackText.text = "";

            }
        
        }

        if(weaponInHand)
        {

            weaponInHand.transform.position = handTransform.position;
            weaponInHand.transform.rotation = handTransform.rotation;

        }

    }

    private void FixedUpdate()
    {

        if(placingTrap && EquippedWeaponID != 0)
        {

            Vector3 tempV3;
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.forward, out hit, TrapPlaceDistance, mask, QueryTriggerInteraction.Ignore))
            {

                tempV3 = hit.point + Vector3.up * 0.25f;

            }
            else
            {

                tempV3 = transform.position + (transform.forward * TrapPlaceDistance);

            }

            switch(TrapScaleMode[EquippedWeaponID])
            {

                case ScalingMode.None:
                placementInfo = NoScaling(tempV3);
                break;

                case ScalingMode.Forward:
                placementInfo = ScaleForward(tempV3);
                break;

                case ScalingMode.Sideways:
                placementInfo = ScaleSideways(tempV3);
                break;

            }

            ghostTrap.SetPositionAndRotation(placementInfo.Item1, placementInfo.Item3);
            if(TrapScaleMode[EquippedWeaponID] != ScalingMode.None)
                ghostTrap.localScale = placementInfo.Item2;

        }
        else
        {

            placingTrap = false;
            if(ghostTrap)
                Destroy(ghostTrap.gameObject);

        }

    }
    
}