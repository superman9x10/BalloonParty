using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterBase player = other.GetComponent<CharacterBase>();
            if (player.getCheckPointList().Contains(gameObject))
            {
                player.canMove = false;
                player.getCheckPointList().Remove(gameObject);
                UIManager.instance.showSelectWeaponUI();

            }

        }
        else
        {
            CharacterBase bot = other.GetComponent<CharacterBase>();
            if(bot.getCheckPointList().Contains(gameObject))
            {
                bot.canMove = false;
                bot.getCheckPointList().Remove(gameObject);
                StartCoroutine(delayMoving((BotController) bot));
            }
        }
    }

    IEnumerator delayMoving(BotController bot)
    {
        int randTime = Random.Range(1, 3);
        yield return new WaitForSeconds(randTime);
        bot.changeWeapon();
        bot.canMove = true;

    }

}
