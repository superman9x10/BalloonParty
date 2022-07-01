using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Balloon : MonoBehaviour
{
    public static event Action<string, Vector3> balloonHitRangeWeapon;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Weapon"))
        {
            balloonHitRangeWeapon?.Invoke("blowUp", gameObject.transform.position);
            Destroy(gameObject);
        }
    }
}
