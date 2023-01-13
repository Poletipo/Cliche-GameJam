using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_01_AnimCtrl : MonoBehaviour {

    [SerializeField] Animator _animator;
    [SerializeField] Mob_01 _mob;

    void Start() {
        _mob.OnStateChanged += OnStateChanged;
    }

    private void OnStateChanged() {
        switch (_mob.CurrentState) {
            case Mob_01.MobState.Idle:
                _animator.SetFloat("Speed", 0);
                break;
            case Mob_01.MobState.Chase:
                _animator.SetFloat("Speed", 1);
                break;
            case Mob_01.MobState.PrepareAttack:
                _animator.SetTrigger("JumpAttackStart");
                _animator.SetInteger("JumpState", 0);
                break;
            case Mob_01.MobState.Attacking:
                _animator.SetInteger("JumpState", 1);
                break;
            case Mob_01.MobState.Stuck:
                _animator.SetInteger("JumpState", 2);
                _animator.SetFloat("Speed", 0);
                break;
            case Mob_01.MobState.Stunned:
                break;
            case Mob_01.MobState.Hurt:
                _animator.SetTrigger("Hurt");
                break;
            case Mob_01.MobState.TargetHurt:
                break;
            case Mob_01.MobState.Dead:
                break;
            default:
                break;
        }
    }

}
