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

    float timer;
    private void OnEnable()
    {
        GameManager.OnGameStateChanged += stateCanMove;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= stateCanMove;
    }

    private void Start()
    {
        base.Start();

    }

    private void Update()
    {
        base.Update();
        movementProcess();
    }

    void randSpeed()
    {
        if(timer <= 0)
        {
            int speed;
            if (player.transform.position.z >= transform.position.z)
            {
                speed = Random.Range(7, 15);
            } else
            {
                speed = Random.Range(5, 9);
            }
            
            timer = timeToRandom;
        } else
        {
            timer -= Time.deltaTime;
        }
        
    }

    protected override void movementProcess()
    {
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
                transform.position +=  dir * movementSpeed * Time.deltaTime;
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
        if (indexPreWeapon != index)
        {
            Destroy(weapon);
            GameObject tmpWeapon = weaponManager.weapons[index];
            Instantiate(tmpWeapon, weaponPivot.transform);

            indexPreWeapon = index;
        }
    }
}
