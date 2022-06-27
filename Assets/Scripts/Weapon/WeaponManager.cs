using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public List<GameObject> weapons; 
    //public List<Weapon> levelList;
    //List<int> weaponType;
    
    //void loadNewWeapon()
    //{
    //    for(int i = 0; i < levelList[GameManager.instance.curLevel].weaponConfigs.Count; i++)
    //    {
    //        if(!weaponType.Contains(levelList[GameManager.instance.curLevel].weaponConfigs[i].numbOfBalloonCanHit))
    //        {
    //            weapons.Add(levelList[GameManager.instance.curLevel].weaponConfigs[i].weaponPrefab);
    //        }
    //    }
    //}
}

[System.Serializable]
public class Weapon
{
    public string level;
    public List<WeaponConfig> weaponConfigs;
}
