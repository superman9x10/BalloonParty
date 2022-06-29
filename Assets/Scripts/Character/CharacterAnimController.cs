using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimController : MonoBehaviour
{
    public CharacterState characterState;
    [SerializeField] Animator animator;
    [SerializeField] CharacterBase character;

    public enum CharacterState
    {
        Idle,
        Moving,
        Attack,
        StopAttack
    }

    private void Update()
    {
        characterStateChannged();
    }

    void characterStateChannged()
    {
        switch(characterState)
        {
            case CharacterState.Idle:
                {
                    characterState = CharacterState.Idle;
                    animator.SetFloat("Speed", 0f);
                    break;
                }
            case CharacterState.Moving:
                {
                    characterState = CharacterState.Moving;
                    animator.SetFloat("Speed", 1f);
                    break;
                }
            case CharacterState.Attack:
                {
                    characterState = CharacterState.Attack;
                    animator.SetBool("isAttack", true);
                    animator.SetInteger("weaponID", character.getWeaponID());
                    break;
                }
            case CharacterState.StopAttack:
                {
                    animator.SetBool("isAttack", false);
                    animator.SetInteger("weaponID", 0);
                    break;
                }
        }
    }


    
}
