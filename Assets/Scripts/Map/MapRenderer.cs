using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRenderer : MonoBehaviour
{
    [SerializeField] GameObject balloonPref;

    private void Start()
    {
        GameObject balloon = Instantiate(balloonPref, transform.position, Quaternion.identity); 
        balloon.transform.parent = transform;
    }
}
