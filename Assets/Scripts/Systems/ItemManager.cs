using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    //Info / ToDo
    /*

    Traps
    id (has to be same as weapon)
    Scaling mode
    Max and Min scale
    Prefab of trap
    Bounds, use a collider on the Root object for the trap to set its bounds so it scales correctly

    Weapon
    id (Same as trap)
    WeaponUseMode
    MeleeRach
    ThrowVelocity
    ShotRange

    ToDo

    - Make interface to add Weapons and Traps to the item catalogue
    - Finish seting up the itemmanager
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

    public enum Rarity
    {

        Common, // 50%
        Uncommon, // 30%
        Rare, // 15%
        Legendary // 5%

    }

    public enum Theme
    {

        Universal,
        Theme1,
        Theme2

    }

    public static ItemManager main {get; private set;}
    [Tooltip("The Name of the Item")]
    public string[] ItemCatalogue;
    public GameObject[] TrapPrefabs, WeaponPrefabs;
    public ScalingMode[] TrapScaleMode;
    public float[] TrapActivationDelay;
    public WeaponUseMode[] AttackMode;
    public Rarity[] ItemRarity;
    public Theme[] ItemTheme;
    public GameObject DroppedWeaponPrefab;

    private void Awake()
    {

        if(ItemManager.main)
        {

            Destroy(ItemManager.main);
            main = this;
            
        }
        else
        {

            main = this;

        }

    }

    public void GivePlayerRandomWeapon(PlayerItemController _playerItemController)
    {

        int randomID = 0;

        Random.InitState(System.DateTime.UtcNow.Second);
        randomID = Random.Range(1, TrapPrefabs.Length);

        _playerItemController.PickedUpWeapon(randomID);

    }

    public void GivePlayerWeaponByID(PlayerItemController _playerItemController, int _weaponID)
    {

        _playerItemController.PickedUpWeapon(_weaponID);

    }

}
