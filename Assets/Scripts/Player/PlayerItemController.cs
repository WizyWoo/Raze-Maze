using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerItemController : MonoBehaviour
{

    /*
    
    TODO
    - Align placement with surface
    - Exessive placement checks for checking all directions to keep traps out of walls
    - Make this use the ItemManager instead of own enums and values
    - Make this independent on Refs to ui

    */

    [Tooltip("This might be a bit heavy")]
    public bool ExstensivePlacementChecks, UseUIFeedback, DesktopMode;
    //References from itemManager
    public DisplayGrabAnchor WeaponDisplay, TrapDisplay;
    public GameObject CurrentWeaponPrefab, CurrentTrapPrefab, TrapDisplayObj, WeaponDisplayObj;
    public ItemManager.ScalingMode CurrentTrapScaleMode;
    public ItemManager.WeaponUseMode CurrentWeaponUseMode;
    public int EquippedWeaponID;
    public float TrapActivationDelay;
    //End
    public float TrapPlaceDistance, PlacementCheckRange, ScaleMaxLenght, ScaleMinLenght, MeleeReach, ThrowVelocity, ShotRange;
    [SerializeField]
    public int CurrentItemID {get; private set;}
    public Text WeaponIDText, FeedbackText;
    public Transform Parent, PlaceTrapFrom;
    public bool PlacingTrap;
    [SerializeField]
    private GameObject droppedWeaponPrefab, activeWeapon;
    [SerializeField]
    private Transform handTransform;
    private bool validLocation;
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
        droppedWeaponPrefab = ItemManager.main.DroppedWeaponPrefab;

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

    public void UpdateItemRefs(int _itemID, GameObject _weaponPrefab, GameObject _trapPrefab, ItemManager.ScalingMode _scalingMode, ItemManager.WeaponUseMode _weaponUseMode, float _activationDelay)
    {

        DropItem(_itemID);

        CurrentWeaponPrefab = _weaponPrefab;
        CurrentTrapPrefab = _trapPrefab;
        CurrentTrapScaleMode = _scalingMode;
        CurrentWeaponUseMode = _weaponUseMode;
        TrapActivationDelay = _activationDelay;
        WeaponDisplay.UpdateDisplayModel(_weaponPrefab);
        TrapDisplay.UpdateDisplayModel(_trapPrefab);

    }

    public void DropItem(int _itemID)
    {

        /*if(CurrentItemID != 0)
        {
            
            GameObject temp = PhotonNetwork.Instantiate(pickupFolderName + droppedWeaponPrefab.name, transform.position, Quaternion.identity);
            temp.GetComponent<WeaponPickup>().WeaponID = CurrentItemID;
            temp.name = "Dropped Weapon ^ " + CurrentItemID;

        }*/
        
        PlacingTrap = false;
        UpdateItemID(_itemID);

    }

    private void UpdateItemID(int _iD)
    {

        CurrentItemID = _iD;

        if(UseUIFeedback)
        {

            if(!WeaponIDText)
                UpdateUIRefs();

            WeaponIDText.text = "^ Weapon ID: " + CurrentItemID + " ^";

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

        _rotation = new Quaternion(0, PlaceTrapFrom.rotation.y, 0, PlaceTrapFrom.rotation.w);

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

        _rotation = new Quaternion(0, PlaceTrapFrom.rotation.y, 0, PlaceTrapFrom.rotation.w);

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
        _rotation = new Quaternion(0, PlaceTrapFrom.rotation.y, 0, PlaceTrapFrom.rotation.w);

        return (_position, Vector3.zero, _rotation);

    }

    #endregion

    public HoldingAnchor EquipWeapon(Transform _vrHand = null)
    {

        if(DesktopMode)
        {

            if(activeWeapon == null)
            {

                activeWeapon = PhotonNetwork.Instantiate(CurrentWeaponPrefab.name, handTransform.position, handTransform.rotation);

                if(activeWeapon.TryGetComponent<Rigidbody>(out Rigidbody _rb))
                {

                    _rb.isKinematic = true;

                }

            }
            else
            {

                EquippedWeaponID = CurrentItemID;
                CurrentItemID = 0;

            }

            return null;

        }
        else if(!PlacingTrap)
        {

            if(activeWeapon)
                PhotonNetwork.Destroy(activeWeapon);

            EquippedWeaponID = CurrentItemID;
            CurrentItemID = 0;
            activeWeapon = PhotonNetwork.Instantiate(CurrentWeaponPrefab.name, _vrHand.position, _vrHand.rotation);
            HoldingAnchor _tempAnchor = activeWeapon.GetComponent<WeaponController>().MainAnchor;
            if(!_tempAnchor)
                _tempAnchor = activeWeapon.GetComponent<HoldingAnchor>();
            _tempAnchor.Grabbed(_vrHand);

            return _tempAnchor;

        }
        else
            return null;

    }

    private void ThrowWeapon()
    {

        //Desktop

        GameObject temp = PhotonNetwork.Instantiate(CurrentWeaponPrefab.name, transform.position, transform.rotation);
        WeaponController controller = temp.GetComponent<WeaponController>();
        controller.WeaponID = CurrentItemID;
        controller.Thrown();
        temp.GetComponent<Rigidbody>().velocity = transform.forward * ThrowVelocity;

        PhotonNetwork.Destroy(activeWeapon);

        UpdateItemID(0);

    }

    private void FireWeapon(bool _fire)
    {

        //VR and Desktop
        activeWeapon.GetComponent<WeaponController>().Activate(_fire);

    }

    public void StartPlacingTrap()
    {

        Debug.Log("2");

        EquippedWeaponID = CurrentItemID;
        CurrentItemID = 0;
        string tempFeedback = "";

        if(!PlacingTrap)
        {

            PlacingTrap = true;
            ghostTrap = Instantiate(CurrentTrapPrefab).transform;
            Destroy(ghostTrap.GetComponent<TrapController>());
            trapBounds = ghostTrap.GetComponent<BoxCollider>().bounds.extents;
            boundsOffset = ghostTrap.GetComponent<BoxCollider>().bounds.center;
            ghostTrap.GetComponent<Collider>().isTrigger = true;

        }
        else if(validLocation)
        {

            Transform temp = PhotonNetwork.Instantiate(CurrentTrapPrefab.name, placementInfo.Item1, placementInfo.Item3).transform;
            if(CurrentTrapScaleMode != ItemManager.ScalingMode.None)
                temp.localScale = placementInfo.Item2;
            temp.gameObject.GetComponent<TrapController>().OnTrapPlaced(TrapActivationDelay, CurrentItemID);
            Destroy(ghostTrap.gameObject);
            UpdateItemID(0);
            tempFeedback = "";

        }
        else
        {

            tempFeedback = "Placement is not valid";
            timer = 2;

        }

        if(UseUIFeedback)
        {

            if(!FeedbackText)
                UpdateUIRefs();
                
            FeedbackText.text = tempFeedback;

        }

    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.Mouse1) && CurrentItemID != 0 && activeWeapon == null)
        {

            StartPlacingTrap();

        }
        else if(Input.GetKeyDown(KeyCode.Mouse0) && CurrentItemID != 0 && activeWeapon)
        {

            switch(CurrentWeaponUseMode)
            {

                case ItemManager.WeaponUseMode.Melee:
                //MeleeAttack();
                break;

                case ItemManager.WeaponUseMode.Throw:
                ThrowWeapon();
                break;

                case ItemManager.WeaponUseMode.Ranged:
                FireWeapon(true);
                break;

            }

        }
        else if(Input.GetKeyUp(KeyCode.Mouse0) && CurrentItemID != 0 && activeWeapon)
        {

            switch(CurrentWeaponUseMode)
            {

                case ItemManager.WeaponUseMode.Melee:
                //MeleeAttack();
                break;

                case ItemManager.WeaponUseMode.Throw:
                ThrowWeapon();
                break;

                case ItemManager.WeaponUseMode.Ranged:
                FireWeapon(false);
                break;

            }

        }

        if(Input.GetKeyDown(KeyCode.R) && CurrentItemID != 0 && !PlacingTrap)
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

        if(activeWeapon && DesktopMode)
        {

            activeWeapon.transform.position = handTransform.position;
            activeWeapon.transform.rotation = handTransform.rotation;

        }

        if(CurrentItemID != 0)
        {

            TrapDisplayObj.SetActive(true);
            WeaponDisplayObj.SetActive(true);

        }
        else
        {

            TrapDisplayObj.SetActive(false);
            WeaponDisplayObj.SetActive(false);

        }

    }

    private void FixedUpdate()
    {

        if(PlacingTrap && EquippedWeaponID != 0)
        {

            Vector3 tempV3;
            RaycastHit hit;
            if(Physics.Raycast(PlaceTrapFrom.position, PlaceTrapFrom.forward, out hit, TrapPlaceDistance, mask, QueryTriggerInteraction.Ignore))
            {

                tempV3 = hit.point + Vector3.up * 0.25f;

            }
            else
            {

                tempV3 = PlaceTrapFrom.position + (PlaceTrapFrom.forward * TrapPlaceDistance);

            }

            switch(CurrentTrapScaleMode)
            {

                case ItemManager.ScalingMode.None:
                placementInfo = NoScaling(tempV3);
                break;

                case ItemManager.ScalingMode.Forward:
                placementInfo = ScaleForward(tempV3);
                break;

                case ItemManager.ScalingMode.Sideways:
                placementInfo = ScaleSideways(tempV3);
                break;

            }

            ghostTrap.SetPositionAndRotation(placementInfo.Item1, placementInfo.Item3);
            if(CurrentTrapScaleMode != ItemManager.ScalingMode.None)
                ghostTrap.localScale = placementInfo.Item2;

        }
        else
        {

            PlacingTrap = false;
            if(ghostTrap)
                Destroy(ghostTrap.gameObject);

        }

    }

}
