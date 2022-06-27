using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponConfig/Create Weapon")]
public class WeaponConfig : ScriptableObject
{
    public string weaponName;
    public int weaponID;
    public float atkRange;
    public int numbOfBalloonCanHit;
    public float bonusMovementSpeed;

    public GameObject weaponPrefab;
}
