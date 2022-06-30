using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FinishLine : MonoBehaviour
{
    [SerializeField] GameObject bonusStage;
    public static FinishLine instance;

    //public static event Action startBonusStage;
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<CharacterBase>().canMove = false;
        if (other.CompareTag("Player"))
        {
            CamController.instance.canChangeToBonusStageOffset = true;
            //startBonusStage?.Invoke();
            //GameManager.instance.updateGameState(GameManager.GameState.Bonus);
        }
        else
        {

            //GameManager.instance.updateGameState(GameManager.GameState.Lose);
        }
    }
}
