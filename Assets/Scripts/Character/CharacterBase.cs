using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class CharacterBase : MonoBehaviour
{
    public static event Action<string, Vector3> hitBallon;

    [Header("BaseConfig")]
    [SerializeField] protected CharacterAnimController animController;
    [SerializeField] protected float movementSpeed;
    public bool canMove;
    public bool autoMove;

    [Header("Weapon")]
    [SerializeField] protected GameObject weaponPivot;
    protected bool isAttack;
    protected GameObject weapon;
    protected int weaponID;
    protected SphereCollider weaponTrigger;
    protected List<GameObject> weaponListToChange;


    [Header("Balloon")]
    [SerializeField] protected string balloonTagToFind;
    [SerializeField] protected List<GameObject> balloonList;
    [SerializeField] LayerMask balloonLayerMask;
    [SerializeField] Collider[] colliders;

    //protected GameObject ballToHit;
    [Header("Map")]
    [SerializeField] protected GameObject map;
    [SerializeField] protected string groundToFind;
    [SerializeField] protected Transform finishLinePos;
    [SerializeField] protected string finishLineToFind;

    [Header("CheckPoint")]
    [SerializeField] protected List<GameObject> checkPointList;

    [Header("SpawnPoint")]
    [SerializeField] Transform spawnPoint;

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += playState;
        Balloon.balloonHitWeapon += checkBalloonInList;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= playState;
        Balloon.balloonHitWeapon -= checkBalloonInList;
    }


    private void Awake()
    {

        weapon = weaponPivot.transform.GetChild(0).gameObject;

        
        weaponID = weapon.GetComponent<WeaponBase>().getWeaponID();

        weaponTrigger = GetComponent<SphereCollider>();
        weaponTrigger.radius = weapon.GetComponent<WeaponBase>().getWeaponRange();

        movementSpeed = weapon.GetComponent<WeaponBase>().getMovementSpeed();
    }

    protected void Update()
    {

        if (weapon == null)
        {
            weapon = weaponPivot.transform.GetChild(0).gameObject;
            weaponID = weapon.GetComponent<WeaponBase>().getWeaponID();

            weaponTrigger = GetComponent<SphereCollider>();
            weaponTrigger.radius = weapon.GetComponent<WeaponBase>().getWeaponRange();
            movementSpeed = weapon.GetComponent<WeaponBase>().getMovementSpeed();
        }
    }

    public void startAttack()
    {
        colliders = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z + transform.forward.z),
            weaponTrigger.radius - 0.025f, balloonLayerMask);
        
        int numbOfBalloon = weapon.GetComponent<WeaponBase>().getNumbOfBalloonCanHit();

        colliders = colliders.OrderBy(x => Vector3.Distance(this.transform.position, x.transform.position)).ToArray();

        if (colliders.Length > numbOfBalloon)
        {
            for (int i = 0; i < numbOfBalloon; i++)
            {
                GameObject bal = colliders[i].gameObject;
                hitBallon?.Invoke("blowUp", bal.transform.position);
                balloonList.Remove(bal);
                Destroy(bal);
            }
        }
        else
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                GameObject bal = colliders[i].gameObject;
                hitBallon?.Invoke("blowUp", bal.transform.position);
                balloonList.Remove(bal);
                Destroy(bal);
            }
        }

        Array.Clear(colliders, 0, colliders.Length);

        autoMove = false;

    }
    public void stopAttack()
    {
        weaponTrigger.enabled = true;
        animController.characterState = CharacterAnimController.CharacterState.StopAttack;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(balloonTagToFind))
        {
            animController.characterState = CharacterAnimController.CharacterState.Attack;

            weaponTrigger.enabled = false;
        } 
    }


    protected void limitMoving()
    {
        float mapSize = map.GetComponent<Renderer>().bounds.size.x;

        var tmp = transform.position;
        tmp.x = Mathf.Clamp(tmp.x, map.transform.position.x - mapSize / 2, map.transform.position.x + mapSize / 2);
        transform.position = tmp;
    }


    public int getWeaponID()
    {
        return weaponID;
    }
    public GameObject getWeapon()
    {
        return weapon;
    }

    protected virtual void movementProcess() { }
    protected virtual void move() { }

    protected void playState(GameManager.GameState state)
    {
        canMove = state == GameManager.GameState.Play;
        switch(state)
        {
            case GameManager.GameState.Ready:
                {
                    transform.position = spawnPoint.position;
                    autoMove = false;
                    break;
                }
            case GameManager.GameState.Play:
                {
                    map = GameObject.FindGameObjectWithTag(groundToFind);

                    createBalloonList();
                    createCheckpointList();
                    finishLinePos = GameObject.FindGameObjectWithTag(finishLineToFind).transform;
                    break;
                }
        }
    }
    protected void rotateProcess(Vector3 dirX)
    {
        Quaternion targetRot = Quaternion.LookRotation(dirX);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, 5 * Time.deltaTime);

    }

    protected void createBalloonList()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(balloonTagToFind);
        balloonList.Clear();

        for (int i = 0; i < gameObjects.Length; i++)
        {
            balloonList.Add(gameObjects[i]);
        }

        balloonList.Sort((x, y) => x.transform.position.z.CompareTo(y.transform.position.z));
    }
    protected void createCheckpointList()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("CheckPoint");
        checkPointList.Clear();

        for (int i = 0; i < gameObjects.Length; i++)
        {
            checkPointList.Add(gameObjects[i]);
        }
    }

    public List<GameObject> getCheckPointList()
    {
        return checkPointList;
    }
    public List<GameObject> getBalloonList()
    {
        return balloonList;
    }

    protected void autoMoveHandle()
    {
        Vector3 dir = (finishLinePos.position - transform.position).normalized;
        dir = new Vector3 (dir.x, 0, dir.z);
        transform.position += dir * movementSpeed * Time.deltaTime;
        animController.characterState = CharacterAnimController.CharacterState.Moving;

        rotateProcess(Vector3.forward);
    }

    protected void checkBalloonInList(GameObject balloon)
    {
        if(balloonList.Contains(balloon)) {
            balloonList.Remove(balloon);
            Destroy(balloon);
        }
    }
}
