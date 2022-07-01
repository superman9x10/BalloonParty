using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        CharacterBase character = other.GetComponent<CharacterBase>();
        if (character.getCheckPointList().Contains(gameObject))
        {
            character.canMove = false;
            character.getCheckPointList().Remove(gameObject);
            
            if(other.CompareTag("Player"))
            {
                UIManager.instance.showSelectWeaponUI();
            } else
            {
                StartCoroutine(delayMoving((BotController) character));
            }
        }
    }

    IEnumerator delayMoving(BotController bot)
    {
        int randTime = Random.Range(1, 2);
        yield return new WaitForSeconds(randTime);
        bot.changeWeapon();
        if(bot.getCheckPointList().Count == 0)
        {
            bot.autoMove = true;
        }
        if(!bot.autoMove)
        {
            bot.canMove = true;
        }
    }

}
