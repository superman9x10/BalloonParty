using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : CharacterBase
{
    //public static event Action<CharacterAnimController.CharacterState, int> OnBotWeaponChanged;

    [Header("BotController")]

    [SerializeField] Transform targetPos;
    [SerializeField] float timeToRandom;
    [SerializeField] GameObject player;

    private void Start()
    {
        base.Start();

    }

    private void Update()
    {
        base.Update();
        movementProcess();
    }

    protected override void movementProcess()
    {
        //if(GameManager.instance.gameState == GameManager.GameState.Ready && Input.GetMouseButtonDown(0))
        //{
        //    canMove = true;
        //}

        move();
        if(map != null)
        {
            limitMoving();
        }
    }
    protected override void move()
    {

        if (canMove)
        {
            if (balloonList.Count != 0)
            {
                targetPos = balloonList[0].transform;
                Vector3 dir = (targetPos.position - transform.position).normalized;
                dir.y = 0;
                transform.position += dir * movementSpeed * Time.deltaTime;
                rotateProcess(dir);
            }

            if (balloonList.Count == 0)
            {
                transform.position += Vector3.forward * movementSpeed * Time.deltaTime;
                rotateProcess(Vector3.forward);
                weaponTrigger.radius = 0.5f;
            }

            animController.characterState = CharacterAnimController.CharacterState.Moving;
        }
        else
        {
            animController.characterState = CharacterAnimController.CharacterState.Idle;
        }
    }

    public void changeWeapon()
    {
        int index = Random.Range(0, weaponManager.weapons.Count);
        Destroy(weapon);
        GameObject tmpWeapon = weaponManager.weapons[index];
        Instantiate(tmpWeapon, weaponPivot.transform);

    }
}
