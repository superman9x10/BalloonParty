using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class WeaponBase : MonoBehaviour
{
    protected string weaponName;
    protected int weaponID;

    protected float atkRange;
    protected int numbOfBalloonCanHit;
    protected float bonusMovementSpeed;

    [SerializeField] WeaponConfig config;
    SphereCollider hitRangeCollider;
    public int getWeaponID()
    {
        return weaponID;
    }

    public float getWeaponRange()
    {
        return atkRange;
    }
    public int getNumbOfBalloonCanHit()
    {
        return numbOfBalloonCanHit;
    }

    public float getMovementSpeed()
    {
        return bonusMovementSpeed;
    }

    private void Awake()
    {
        weaponName = config.weaponName;
        weaponID= config.weaponID;
        atkRange = config.atkRange;
        numbOfBalloonCanHit = config.numbOfBalloonCanHit;
        bonusMovementSpeed = config.bonusMovementSpeed;
    }

}
