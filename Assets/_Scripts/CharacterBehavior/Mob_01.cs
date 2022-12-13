using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_01 : MonoBehaviour {

    public enum MobState {
        Idle,
        Chase,
        PrepareAttack,
        Attacking,
        Stuck,
        Stunned,
        Hurt,
        TargetHurt,
        Dead
    }

    public Action OnStateChanged;

    [SerializeField] AudioClip _jumpSFX;
    [SerializeField] Health _health;
    public AnimationCurve JumpAnimCurve;
    public GameObject DeathParticle;
    public AudioClip hurtSFX;
    public IHitter Hitter;
    public float MinChaseTime = 3;
    public float AttackDistance = 5;
    public float PrepareTime = 2;
    public float LoungeAttackTime = 1;
    public float StuckTime = 2;
    public float KnockBackTime = 0.5f;
    public float ChaseDistance;

    private MovementController _mc;
    private GameObject player;
    private Vector3 _previousPosition;
    private Vector3 _targetPosition;
    private float _chaseStartTime;
    private float _prepareStartTime;
    private float _startAttackTime;
    private float _stuckTimer;
    private float _idleTime = 1;
    private float _idleStartTime = 0;
    private float knockBackStartTime;

    private MobState _currenState = MobState.Idle;

    public MobState CurrentState {
        get { return _currenState; }
        set {

            if (value == MobState.Chase) {
                Hitter.Activate();
            }
            else if (value == MobState.Stuck) {
                Hitter.Deactivate();
            }
            else if (value == MobState.Attacking) {
                AudioManager.Instance.PlayAudio(_jumpSFX, transform.position);
            }

            _currenState = value;
            OnStateChanged?.Invoke();
        }
    }

    void Start() {
        _mc = GetComponent<MovementController>();
        _idleStartTime = Time.time;

        _health.OnDeath += OnDeath;
        _health.OnHurt += OnHurt;

        Hitter.Activate();
        Hitter.OnHit += OnTargetHit;

        player = GameManager.Instance.Player;

    }

    private void OnTargetHit() {
        Hitter.Deactivate();
        StartCoroutine(ReactivateHitter(1f));
    }

    IEnumerator ReactivateHitter(float time) {
        yield return new WaitForSeconds(time);
        Hitter.Activate();
    }

    private void OnDeath() {
        StartCoroutine(OnDeathWait());
    }

    private IEnumerator OnDeathWait() {
        Hitter.Deactivate();
        yield return new WaitForSeconds(.2f);
        Instantiate(DeathParticle, transform.position + (Vector3.up), Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnHurt() {
        AudioManager.Instance.PlayAudio(hurtSFX, transform.position);
        _mc.MoveInput = Vector2.zero;
        CurrentState = MobState.Hurt;

        knockBackStartTime = Time.time;
    }

    void Update() {
        switch (CurrentState) {

            case MobState.Idle:
                IdleState();
                break;
            case MobState.Chase:
                ChaseState();
                break;
            case MobState.PrepareAttack:
                PrepareAttack();
                break;
            case MobState.Attacking:
                AttackState();
                break;
            case MobState.Stuck:
                StuckState();
                break;
            case MobState.Hurt:
                HurtState();
                break;
            default:
                break;
        }

    }

    private void OnDestroy() {
        _health.OnHurt -= OnHurt;
        _health.OnDeath -= OnDeath;
    }

    private void HurtState() {
        float t = Mathf.Clamp01((Time.time - knockBackStartTime) / KnockBackTime);
        if (t >= 1) {
            _idleStartTime = Time.time;
            CurrentState = MobState.Idle;
            return;
        }
    }

    private void StuckState() {
        if (_stuckTimer + StuckTime < Time.time) {
            _chaseStartTime = Time.time;
            CurrentState = MobState.Chase;
            return;
        }
    }

    private void AttackState() {

        float t = Mathf.Clamp01((Time.time - _startAttackTime) / LoungeAttackTime);

        if (t >= 1) {
            _stuckTimer = Time.time;
            CurrentState = MobState.Stuck;
            return;
        }

        Vector3 groundPos = Vector3.Lerp(_previousPosition, _targetPosition, t);
        groundPos.y = JumpAnimCurve.Evaluate(t);

        transform.position = groundPos;
    }

    private void IdleState() {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (_idleTime + _idleStartTime < Time.time && distance <= ChaseDistance) {
            CurrentState = MobState.Chase;
            _chaseStartTime = Time.time;
            return;
        }
    }

    private void PrepareAttack() {

        if (_prepareStartTime + PrepareTime < Time.time) {
            _startAttackTime = Time.time;
            _previousPosition = transform.position;
            _previousPosition.y = 0;
            CurrentState = MobState.Attacking;
            return;
        }
    }

    private void ChaseState() {

        //Change state possibilities
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (_chaseStartTime + MinChaseTime < Time.time && distance <= AttackDistance) {

            _targetPosition = player.transform.position;
            _targetPosition.y = 0;


            _mc.MoveInput = Vector2.zero;

            CurrentState = MobState.PrepareAttack;
            _prepareStartTime = Time.time;
            return;
        }

        Vector3 direction = player.transform.position - transform.position;
        Vector2 directionInput = new Vector2(direction.x, direction.z).normalized;
        _mc.MoveInput = directionInput;

    }
}
