using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.instance.updateGameState(GameManager.GameState.Win);
        } else
        {
            GameManager.instance.updateGameState(GameManager.GameState.Lose);
        }
    }
}
