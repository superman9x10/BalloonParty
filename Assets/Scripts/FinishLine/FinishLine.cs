using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FinishLine : MonoBehaviour
{
    [SerializeField] GameObject bonusStage;
    public static FinishLine instance;

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Weapon"))
        {
            other.GetComponent<CharacterBase>().canMove = false;
            other.GetComponent<CharacterBase>().autoMove = false;
        }

        if (other.CompareTag("Player"))
        {
            CamController.instance.canChangeToBonusStageOffset = true;
            
        }
        else if(!other.CompareTag("Weapon"))
        {
            other.GetComponent<BotController>().isHitFinishLine = true;
            //GameManager.instance.updateGameState(GameManager.GameState.Lose);
        }
    }
}
