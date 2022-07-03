using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BonusUI : MonoBehaviour
{
    public static BonusUI instance;
    [SerializeField] Slider slider;
    [SerializeField] ProgressBarPro progressBar;

    public bool isSetValue;
    public bool canThrow;
    public float throwForce;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        //slider.value = Mathf.Abs(Mathf.Sin(Time.time * 5f));
        if(!isSetValue)
        {
            progressBar.SetValue(Mathf.Abs(Mathf.Sin(Time.time * 5)));
        }
        
        
        if(Input.GetMouseButtonDown(0) && !isSetValue)
        {
            isSetValue = true;
            canThrow = true;
            throwForce = progressBar.Value * 170;
        }
    }
}
