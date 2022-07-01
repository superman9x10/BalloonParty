using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FinishLine : MonoBehaviour
{
    [SerializeField] GameObject bonusStage;
    public static FinishLine instance;

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<CharacterBase>().canMove = false;
        other.GetComponent<CharacterBase>().autoMove = false;
        if (other.CompareTag("Player"))
        {
            CamController.instance.canChangeToBonusStageOffset = true;
            
        }
        else
        {

            //GameManager.instance.updateGameState(GameManager.GameState.Lose);
        }
    }
}
