using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;
    public static event Action<GameState> OnGameStateChanged;
    public int curLevel;
    public GameState gameState;


    public enum GameState
    {
        Ready,
        Play,
        Win,
        Lose,
        Bonus,
        EndGame
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }


    private void Start()
    {
        updateGameState(GameState.Ready);

    }

    public void updateGameState(GameState newState)
    {
        gameState = newState;
        switch(newState)
        {
            case GameState.Ready:
                {
                    CamController.instance.canChangeToBonusStageOffset = false;
                    //Debug.Log("Ready");
                    break;
                }
            case GameState.Play:
                {
                    //Debug.Log("Play");
                    break;
                }
            case GameState.Win:
                {
                    Debug.Log("Win");
                    break;
                }
            case GameState.Lose:
                {
                    Debug.Log("Lose");
                    break;
                }
            case GameState.Bonus:
                {
                    Debug.Log("Bonus");
                    break;
                }
            case GameState.EndGame:
                {
                    //Debug.Log("End");
                    break;
                }
        }

        OnGameStateChanged?.Invoke(newState);
    }



}
