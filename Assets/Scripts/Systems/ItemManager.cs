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
    private List<int> commonItems, unCommonItems, rareItems, legendaryItems;

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

    private void Start()
    {

        commonItems = new List<int>();
        unCommonItems = new List<int>();
        rareItems = new List<int>();
        legendaryItems = new List<int>();

        for(int i = 1; i < ItemCatalogue.Length; i++)
        {

            switch(ItemRarity[i])
            {

                case Rarity.Common:
                commonItems.Add(i);
                break;

                case Rarity.Uncommon:
                unCommonItems.Add(i);
                break;

                case Rarity.Rare:
                rareItems.Add(i);
                break;

                case Rarity.Legendary:
                legendaryItems.Add(i);
                break;

            }
            
        }

    }

    public void GivePlayerRandomWeapon(PlayerItemController _playerItemController)
    {

        int randomID = 0;

        Random.InitState(System.DateTime.UtcNow.Second);
        randomID = Random.Range(1, TrapPrefabs.Length);

        _playerItemController.UpdateItemRefs(randomID, WeaponPrefabs[randomID], TrapPrefabs[randomID], TrapScaleMode[randomID], AttackMode[randomID], TrapActivationDelay[randomID]);

    }

    public void GivePlayerWeaponByID(PlayerItemController _playerItemController, int _itemID)
    {

        _playerItemController.UpdateItemRefs(_itemID, WeaponPrefabs[_itemID], TrapPrefabs[_itemID], TrapScaleMode[_itemID], AttackMode[_itemID], TrapActivationDelay[_itemID]);

    }

    public void GiveRandomFilteredID(Theme _theme, PlayerItemController _playerItemController)
    {

        Random.InitState(System.DateTime.UtcNow.Millisecond);
        int _rarity = Random.Range(0, 101);
        int _returnID = 0;
        Rarity _tempRarity = Rarity.Common;

        if(_rarity > 50 && _rarity <= 80)
        {

            _tempRarity = Rarity.Uncommon;

        }
        else if(_rarity > 80 && _rarity <= 95)
        {

            _tempRarity = Rarity.Rare;

        }
        else if(_rarity > 95)
        {

            _tempRarity = Rarity.Legendary;

        }

        bool _idChosen = false;

        while(!_idChosen)
        {

            if(_tempRarity == Rarity.Common)
            {

                _returnID = commonItems[Random.Range(0, commonItems.Count)];
                if(ItemTheme[_returnID] == _theme || ItemTheme[_returnID] == Theme.Universal)
                {

                    _idChosen = true;

                }

            }
            else if(_tempRarity == Rarity.Uncommon)
            {

                _returnID = unCommonItems[Random.Range(0, unCommonItems.Count)];
                if(ItemTheme[_returnID] == _theme || ItemTheme[_returnID] == Theme.Universal)
                {

                    _idChosen = true;

                }

            }
            else if(_tempRarity == Rarity.Rare)
            {

                _returnID = rareItems[Random.Range(0, rareItems.Count)];
                if(ItemTheme[_returnID] == _theme || ItemTheme[_returnID] == Theme.Universal)
                {

                    _idChosen = true;

                }

            }
            else if(_tempRarity == Rarity.Legendary)
            {

                _returnID = legendaryItems[Random.Range(0, legendaryItems.Count)];
                if(ItemTheme[_returnID] == _theme || ItemTheme[_returnID] == Theme.Universal)
                {

                    _idChosen = true;

                }

            }

        }

        _playerItemController.UpdateItemRefs(_returnID, WeaponPrefabs[_returnID], TrapPrefabs[_returnID], TrapScaleMode[_returnID], AttackMode[_returnID], TrapActivationDelay[_returnID]);

    }

}
