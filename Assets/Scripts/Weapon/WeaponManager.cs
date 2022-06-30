using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;
    public List<GameObject> weapons;

    public List<WeaponConfig> rangeWeapon;
    public List<WeaponConfig> meleeWeapon;

    CharacterBase player;
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
    }

    private void OnDisable()
    {
        LoadWeaponToUI.onEnableWeaponUI -= updateWeaponList;
    }
    void updateWeaponList()
    {
        weapons.Clear();
        

        if (player.getCheckPointList().Count == 0)
        {
            Debug.Log("Last1");
            foreach (var weapon in rangeWeapon)
            {
                weapons.Add(weapon.weaponPrefab);
            }
        }
        else
        {
            Debug.Log("Last2");
            foreach (var weapon in meleeWeapon)
            {
                weapons.Add(weapon.weaponPrefab);
            }
        }
        
    }
}


