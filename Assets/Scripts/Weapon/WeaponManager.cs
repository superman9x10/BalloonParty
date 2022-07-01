using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;
    public List<GameObject> weapons;

    public List<WeaponConfig> rangeWeapon;
    public List<WeaponConfig> meleeWeapon;

    public List<GameObject> meleeWeaponListToSwap;
    public List<GameObject> rangeWeaponListToSwap;

    CharacterBase player;
    //public bool isLastCheckPoint;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBase>();
    }

    private void OnEnable()
    {
        LoadWeaponToUI.onEnableWeaponUI += updateWeaponList;
        GameManager.OnGameStateChanged += updateWeaponWhenGameReady;
    }

    private void OnDisable()
    {
        LoadWeaponToUI.onEnableWeaponUI -= updateWeaponList;
        GameManager.OnGameStateChanged -= updateWeaponWhenGameReady;
    }


    void updateWeaponList()
    {
        weapons.Clear();
        

        if (player.getCheckPointList().Count == 0)
        {
            foreach (var weapon in rangeWeapon)
            {
                weapons.Add(weapon.weaponPrefab);
            }
        }
        else
        {
            if(weapons.Count == 0)
            {
                foreach (var weapon in meleeWeapon)
                {
                    weapons.Add(weapon.weaponPrefab);
                }
            }
            
        }
    }

   

    void updateWeaponWhenGameReady(GameManager.GameState state)
    {
        if(state == GameManager.GameState.Ready)
        {
            weapons.Clear();
            foreach (var weapon in meleeWeapon)
            {
                weapons.Add(weapon.weaponPrefab);
            }
        }
    }

    public List<GameObject> getMeleeWeapon()
    {
        for(int i = 0; i < 3; i++)
        {
            int randIndex = Random.Range(0, meleeWeapon.Count);
            if(!meleeWeaponListToSwap.Contains(meleeWeapon[i].weaponPrefab))
            {
                meleeWeaponListToSwap.Add(meleeWeapon[i].weaponPrefab);
            }
        }
        return meleeWeaponListToSwap;
    }
    public List<GameObject> getRangeWeapon()
    {
        for (int i = 0; i < 3; i++)
        {
            int randIndex = Random.Range(0, rangeWeapon.Count);
            if (!rangeWeaponListToSwap.Contains(rangeWeapon[i].weaponPrefab))
            {
                rangeWeaponListToSwap.Add(rangeWeapon[i].weaponPrefab);
            }
        }
        return rangeWeaponListToSwap;
    }
}


