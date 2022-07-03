using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Balloon : MonoBehaviour
{
    public static event Action<string, Vector3> balloonHitRangeWeapon;
    public static event Action<GameObject> balloonHitWeapon;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Weapon"))
        {
            balloonHitRangeWeapon?.Invoke("blowUp", gameObject.transform.position);
            balloonHitWeapon?.Invoke(gameObject);
            
            //Destroy(gameObject);
        }
    }
}
