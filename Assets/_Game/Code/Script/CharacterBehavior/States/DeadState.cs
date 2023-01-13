using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : BossState {

    [SerializeField] Animator _animator;
    [SerializeField] ParticleSystem cloudParticles;

    public AudioSource WinMusic;

    public override void EnterState(BossState previousState) {
        WinMusic.Play();
        _animator.CrossFade("RIG_Boss_01|Boss_Death", .2f);
        cloudParticles.Play();
        GameManager.Instance.WinGame();
    }

    public override void LeaveState() { }

    public override BossState UpdateState() {
        return null;
    }
}
