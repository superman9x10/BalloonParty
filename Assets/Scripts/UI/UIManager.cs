using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject selectWeaponUI;
    private PlayerController player;
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
                    break;
                }
            case GameManager.GameState.Play:
                {
                    break;
                }
            case GameManager.GameState.Win:
                {
                    break;
                }
            case GameManager.GameState.Lose:
                {
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

    public void afterSelectWeapon()
    {
        CharacterBase player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBase>();
        player.canMove = true;
        
    }

    public void btnWeaponChange()
    {
        GameObject tmpBtn = EventSystem.current.currentSelectedGameObject;
        int tmpBtnIndex = tmpBtn.transform.GetSiblingIndex();
        player.changeWeapon(tmpBtnIndex);
    }

}
