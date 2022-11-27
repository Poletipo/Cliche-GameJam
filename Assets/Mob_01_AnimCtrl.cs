using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_01_AnimCtrl : MonoBehaviour
{
    [SerializeField]
    Animator _animator;
    [SerializeField]
    Mob_01 mob;

    // Start is called before the first frame update
    void Start()
    {
        mob.OnStateChanged += OnStateChanged;
    }

    private void OnStateChanged()
    {
        switch (mob.CurrentState)
        {
            case Mob_01.Boss_01States.Idle:
                _animator.SetFloat("Speed", 0);
                break;
            case Mob_01.Boss_01States.Chase:
                _animator.SetFloat("Speed", 1);
                break;
            case Mob_01.Boss_01States.PrepareAttack:
                _animator.SetTrigger("JumpAttackStart");
                _animator.SetInteger("JumpState", 0);
                break;
            case Mob_01.Boss_01States.Attacking:
                _animator.SetInteger("JumpState", 1);
                break;
            case Mob_01.Boss_01States.Stuck:
                _animator.SetInteger("JumpState", 2);
                _animator.SetFloat("Speed", 0);
                break;
            case Mob_01.Boss_01States.Stunned:
                break;
            case Mob_01.Boss_01States.Hurt:
                _animator.SetTrigger("Hurt");
                break;
            case Mob_01.Boss_01States.TargetHurt:
                break;
            case Mob_01.Boss_01States.Dead:
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
