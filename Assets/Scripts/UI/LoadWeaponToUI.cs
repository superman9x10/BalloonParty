using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LoadWeaponToUI : MonoBehaviour
{
    [SerializeField] List<GameObject> weaponBtns;

    [SerializeField] GameObject btnGroup;

    public static event Action onEnableWeaponUI;

    private void OnEnable()
    {
        onEnableWeaponUI?.Invoke();

        for (int i = 0; i < btnGroup.transform.childCount; i++)
        {
            GameObject btn = btnGroup.transform.GetChild(i).transform.GetChild(0).gameObject;
            btn.GetComponent<Text>().text = WeaponManager.instance.weapons[i].name;
        }
    }

}
