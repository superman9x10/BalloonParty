using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<CharacterBase>().canMove = false;
            GameManager.instance.updateGameState(GameManager.GameState.Win);
        }
        else
        {
            other.GetComponent<CharacterBase>().canMove = false;
            GameManager.instance.updateGameState(GameManager.GameState.Lose);
        }
    }
}
