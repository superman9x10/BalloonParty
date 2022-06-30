using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BonusUI : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] ProgressBarPro abc;

    private void Update()
    {
        //slider.value = Mathf.Abs(Mathf.Sin(Time.time * 5f));
        abc.SetValue(Mathf.Abs(Mathf.Sin(Time.time * 4)));
        Debug.Log(abc.Value);
    }
}
