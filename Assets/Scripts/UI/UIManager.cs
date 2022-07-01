using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject selectWeaponUI;
    [SerializeField] GameObject WinUI;
    [SerializeField] GameObject bonusStageUI;

    public static UIManager instance;
    private PlayerController player;
    private void Awake()
    {
        instance = this;
    }
    private void OnEnable()
    {
        GameManager.OnGameStateChanged += managerUI;
    }
    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= managerUI;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void managerUI(GameManager.GameState state)
    {
        switch (state)
        {
            case GameManager.GameState.Ready:
                {
                    hideUI(WinUI);
                    hideUI(bonusStageUI);
                    break;
                }
            case GameManager.GameState.Play:
                {
                    
                    break;
                }
            case GameManager.GameState.Win:
                {
                    //showUI(WinUI);
                    break;
                }
            case GameManager.GameState.Lose:
                {
                    break;
                }
            case GameManager.GameState.Bonus:
                {
                    break;
                }
            case GameManager.GameState.EndGame:
                {
                    showUI(WinUI);
                    hideUI(bonusStageUI);
                    break;
                }
        }
    }

    public void hideUI(GameObject UI)
    {
        UI.SetActive(false);
    }
    public void showUI(GameObject UI)
    {
        UI.SetActive(true);
    }

    public void showSelectWeaponUI()
    {
        showUI(selectWeaponUI);
    }

    public void showBonusStageUI()
    {
        showUI(bonusStageUI);
    }

    public void afterSelectWeapon()
    {
        
        CharacterBase player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBase>();
        
        if(player.getCheckPointList().Count == 0)
        {
              player.autoMove = true;
        }

        if (!player.autoMove)
        {
            player.canMove = true;
        }
        
    }

    public void btnWeaponChange()
    {
        GameObject tmpBtn = EventSystem.current.currentSelectedGameObject;
        int tmpBtnIndex = tmpBtn.transform.GetSiblingIndex();
        player.changeWeapon(tmpBtnIndex);
    }

    public void nextLevel()
    {
        if(GameManager.instance.curLevel < MapManager.instance.levelList.Count - 1)
        {
            GameManager.instance.curLevel++;
        } else
        {
            GameManager.instance.curLevel = 0;
        }

        GameManager.instance.updateGameState(GameManager.GameState.Ready);
    }
}
