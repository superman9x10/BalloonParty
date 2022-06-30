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

    [Header("Weapon")]
    [SerializeField] protected GameObject weaponPivot;
    protected bool isAttack;
    protected GameObject weapon;
    protected int weaponID;
    protected SphereCollider weaponTrigger;

    protected WeaponManager weaponManager;


    [Header("Balloon")]
    [SerializeField] protected string balloonTagToFind;
    [SerializeField] protected List<GameObject> balloonList;
    [SerializeField] LayerMask balloonLayerMask;
    [SerializeField] Collider[] colliders;

    //protected GameObject ballToHit;
    [Header("Map")]
    [SerializeField] protected GameObject map;
    [SerializeField] protected string groundToFind;

    [Header("CheckPoint")]
    [SerializeField] protected List<GameObject> checkPointList;

    [Header("SpawnPoint")]
    [SerializeField] Transform spawnPoint;

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += playState;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= playState;
    }


    private void Awake()
    {

        weapon = weaponPivot.transform.GetChild(0).gameObject;

        
        weaponID = weapon.GetComponent<WeaponBase>().getWeaponID();

        weaponTrigger = GetComponent<SphereCollider>();
        weaponTrigger.radius = weapon.GetComponent<WeaponBase>().getWeaponRange();

        movementSpeed = weapon.GetComponent<WeaponBase>().getMovementSpeed();
    }
    protected void Start()
    {

        weaponManager = GameObject.Find("weaponManager").GetComponent<WeaponManager>();
        
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

    protected virtual void movementProcess() { }
    protected virtual void move() { }

    protected void playState(GameManager.GameState state)
    {
        canMove = state == GameManager.GameState.Play;

        if (state == GameManager.GameState.Ready)
        {
            transform.position = spawnPoint.position;
        }
        
        if (state == GameManager.GameState.Play)
        {
            map = GameObject.FindGameObjectWithTag(groundToFind);

            createBalloonList();
            createCheckpointList();
            
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


}
