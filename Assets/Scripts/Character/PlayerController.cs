using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerController : CharacterBase
{
   // public static event Action<CharacterAnimController.CharacterState ,int> OnPlayerWeaponChanged;

    [Header("PlayerConfig")]
    [SerializeField] float sensitivityX;
    [SerializeField] FloatingJoystick joystick;


    private void OnEnable()
    {
        GameManager.OnGameStateChanged += stateCanMove;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= stateCanMove;
    }


    float mouseX;
    Vector3 dirX;
    float touchPosX;

    void Update()
    {
        base.Update();

        movementProcess();
        //movement();
    }

    void movement()
    {
        limitMoving();
        if ((Mathf.Abs(joystick.Horizontal) > 0.01f || Mathf.Abs(joystick.Vertical) > 0.01f) && canMove)
        {

            transform.position += new Vector3(joystick.Horizontal * movementSpeed * Time.deltaTime, 0, joystick.Vertical * movementSpeed * Time.deltaTime);
            Vector3 dirToLook = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dirToLook), 10 * Time.deltaTime);

            if (balloonList.Count == 0)
            {
                weaponTrigger.radius = 0.5f;
            }

            animController.characterState = CharacterAnimController.CharacterState.Moving;
        }
        else
        {
            animController.characterState = CharacterAnimController.CharacterState.Idle;
        }
    }

    protected override void movementProcess()
    {
        if(GameManager.instance.gameState == GameManager.GameState.Ready && Input.GetMouseButtonDown(0))
        {
            GameManager.instance.updateGameState(GameManager.GameState.Play);
        } 

        if (Input.GetMouseButton(0) && canMove)
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            mouseX = Input.GetAxis("Mouse X");

            mouseX = Mathf.Clamp(mouseX, -1f, 1f);

            dirX = new Vector3(mouseX, 0, 0);

            move();

            if (map != null)
            {
                limitMoving();
            }

            rotateProcess(dirX);
            animController.characterState = CharacterAnimController.CharacterState.Moving;
        }
        else
        {
            animController.characterState = CharacterAnimController.CharacterState.Idle;
        }

    }

    protected override void move()
    {
        transform.position += Vector3.forward * movementSpeed * Time.deltaTime;

        touchPosX += mouseX * sensitivityX * Time.deltaTime;
        touchPosX = Mathf.Clamp(touchPosX, -3.2f, 2.8f);
        
        transform.position = new Vector3(touchPosX, transform.position.y, transform.position.z);
        //limitMoving();

    }

    public void changeWeapon(int weaponIndex)
    {
        if (indexPreWeapon != weaponIndex)
        {
            Destroy(weapon);
            GameObject tmpWeapon = weaponManager.weapons[weaponIndex];
            Instantiate(tmpWeapon, weaponPivot.transform);

            indexPreWeapon = weaponIndex;
        }
    }

}
