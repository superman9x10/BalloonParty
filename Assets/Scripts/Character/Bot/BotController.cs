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

    public Transform attackPoint;
    public GameObject objectToThrow;
    public bool isHitFinishLine;
    private void Start()
    {
        //base.Start();

    }

    private void Update()
    {
        base.Update();
        movementProcess();
        finishLineHandle();
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
        else if (autoMove)
        {
            autoMoveHandle();
        }
        else
        {
            animController.characterState = CharacterAnimController.CharacterState.Idle;
        }
    }

    public void changeWeapon()
    {
        //int index = Random.Range(0, WeaponManager.instance.weapons.Count);
        //Destroy(weapon);
        //GameObject tmpWeapon = WeaponManager.instance.weapons[index];
        //Instantiate(tmpWeapon, weaponPivot.transform);

        Destroy(weapon);
        int index = Random.Range(0, WeaponManager.instance.weapons.Count);
        GameObject tmpWeapon;
        if (checkPointList.Count == 0)
        {
            tmpWeapon = WeaponManager.instance.getRangeWeapon()[index];
        }
        else
        {
            tmpWeapon = WeaponManager.instance.getMeleeWeapon()[index];
        }

        Instantiate(tmpWeapon, weaponPivot.transform);

    }

    void finishLineHandle()
    {
        if(isHitFinishLine)
        {
            isHitFinishLine = false;
            StartCoroutine(startThrow());
        }
    }

    IEnumerator startThrow()
    {
        objectToThrow = gameObject.GetComponent<CharacterBase>().getWeapon();

        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, Quaternion.Euler(new Vector3(180, 0, 0)));
        projectile.AddComponent<BoxCollider>();
        projectile.AddComponent<Rigidbody>();
        projectile.GetComponent<BoxCollider>().isTrigger = true;
        projectile.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        projectile.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;

        int force = Random.Range(45, 170);
        int randTime = Random.Range(1, 3);
        yield return new WaitForSeconds(randTime);

        float startTime = Time.time;
        while (Time.time < startTime + 0.65f)
        {
            projectile.transform.position += force * Time.deltaTime * Vector3.forward;
            yield return null;
        }
        Destroy(projectile);
    }
}
