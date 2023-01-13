using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BossState {

    [SerializeField] BlockState _blockState;
    [SerializeField] IdleState _idleState;
    [SerializeField] Animator _animator;
    [SerializeField] Health _health;

    public IHitter[] hitters;
    public string[] attackAnimName;
    public int AttackCount = 2;

    private bool isAttackDone = false;
    private int _attackCounter = 0;
    private int _randomAttack = 0;

    private void Start() {
        for (int i = 0; i < hitters.Length; i++) {
            hitters[i].OnHit += OnTargetHit;
        }

        _health.OnHurt += OnHurt;
    }

    private void OnHurt() {
        AttackCount += 3;
    }

    private void OnTargetHit() {

        for (int i = 0; i < hitters.Length; i++) {
            hitters[i].Deactivate();
        }

        _attackCounter--;
        isAttackDone = true;
    }

    public void AnimationOver() {
        isAttackDone = true;
    }

    public override void EnterState(BossState previousState) {

        _randomAttack = Random.Range(0, attackAnimName.Length);
        _animator.CrossFade(attackAnimName[_randomAttack], 0.2f);
    }

    public override BossState UpdateState() {
        if (isAttackDone) {
            _attackCounter++;
            if (_attackCounter >= AttackCount) {
                _attackCounter = 0;
                return _blockState;
            }

            return _idleState;
        }

        return null;
    }

    public override void LeaveState() {
        isAttackDone = false;
    }
}
