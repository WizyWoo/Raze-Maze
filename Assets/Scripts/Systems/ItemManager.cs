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

    public static ItemManager main {get; private set;}
    public string[] ItemCatalogue;
    public GameObject[] TrapPrefabs, WeaponPrefabs;
    public ScalingMode[] TrapScaleMode;
    public float[] TrapActivationDelay;
    public WeaponUseMode[] AttackMode;
    public Rarity[] ItemRarity;

    private void Awake()
    {

        main = this;

    }

    public int RandomItemID()
    {

        int randomID = 0;

        return randomID;

    }

}
