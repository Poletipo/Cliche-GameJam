using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_01 : MonoBehaviour
{

    public enum Boss_01States
    {
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


    public float minChaseTime = 3;
    private float _chaseTimerStart;


    public float AttackDistance = 5;
    public float PrepareTime = 2;
    float _prepareTimer;

    public float loungeAttackTime = 1;
    float startAttackTime;
    public AnimationCurve JumpAnimCurve;

    public float stuckTime = 2;
    private float _stuckTimer;

    float IdleTime = 1;
    float _idleStartTime= 0;

    public float knockBackTime = 0.5f;
    private float knockBackStartTime;
    public GameObject DeathParticle;



    private Boss_01States _currenState = Boss_01States.Idle;

    public Boss_01States CurrentState
    {
        get { return _currenState; }
        set { 

            if(value == Boss_01States.Chase)
            {
                hitter.Activate();
            }
            else if(value == Boss_01States.Stuck)
            {
                hitter.Deactivate();
            }

            _currenState = value; 
            OnStateChanged?.Invoke();
        }
    }

    private GameObject player;
    [SerializeField]
    private Health _health;


    public IHitter hitter;

    MovementController _mc;
    private Vector3 previousPosition;
    private Vector3 targetPosition;
    public float ChaseDistance;



    // Start is called before the first frame update
    void Start()
    {
        _mc = GetComponent<MovementController>();
        _idleStartTime = Time.time;


        _health.OnDeath += OnDeath;
        _health.OnHurt += OnHurt;

        hitter.Activate();
        hitter.OnHit += OnTargetHit;

        player = GameManager.Instance.Player;

    }

    private void OnTargetHit()
    {
        hitter.Deactivate();
        //_idleStartTime = Time.time;
        //CurrentState = Boss_01States.Idle;
        StartCoroutine(ReactivateHitter(1f));
    }


    IEnumerator ReactivateHitter(float time)
    {
        yield return new WaitForSeconds(time);
        hitter.Activate();
    }

    private void OnDeath()
    {
        StartCoroutine(OnDeathWait());
    }

    private IEnumerator OnDeathWait()
    {
        hitter.Deactivate();
        yield return new WaitForSeconds(.2f);
        Instantiate(DeathParticle, transform.position + (Vector3.up), Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnHurt()
    {
        _mc.MoveInput = Vector2.zero;
        CurrentState = Boss_01States.Hurt;

        knockBackStartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        switch (CurrentState)
        {

            case Boss_01States.Idle:
                IdleState();
                break;
            case Boss_01States.Chase:
                ChaseState();
                break;
            case Boss_01States.PrepareAttack:
                PrepareAttack();
                break;
            case Boss_01States.Attacking:
                AttackState();
                break;
            case Boss_01States.Stuck:
                StuckState();
                break;
            case Boss_01States.Hurt:
                HurtState();
                break;
            default:
                break;
        }

    }
    private void OnDestroy()
    {
        _health.OnHurt -= OnHurt;
        _health.OnDeath -= OnDeath;
    }

    private void HurtState()
    {
        float t = Mathf.Clamp01((Time.time - knockBackStartTime) / knockBackTime);
        if (t >= 1)
        {
            _idleStartTime = Time.time;
            CurrentState = Boss_01States.Idle;
            return;
        }
    }

    private void StuckState()
    {
        if (_stuckTimer + stuckTime < Time.time)
        {
            _chaseTimerStart = Time.time;
            CurrentState = Boss_01States.Chase;
            return;
        }
    }

    private void AttackState()
    {

        float t = Mathf.Clamp01((Time.time - startAttackTime) / loungeAttackTime);

        if(t >= 1)
        {
            _stuckTimer = Time.time;
            CurrentState = Boss_01States.Stuck;
            return;
        }

        Vector3 groundPos = Vector3.Lerp(previousPosition, targetPosition, t);
        groundPos.y = JumpAnimCurve.Evaluate(t);

        //groundPos.y = -Mathf.Pow((t-.5f)*2 ,2) + 1;

        transform.position = groundPos;
    }

    private void IdleState()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (IdleTime + _idleStartTime < Time.time && distance <= ChaseDistance)
        {
            CurrentState = Boss_01States.Chase;
            _chaseTimerStart = Time.time;
            return;
        }
    }

    private void PrepareAttack()
    {

        if (_prepareTimer + PrepareTime < Time.time)
        {
            startAttackTime = Time.time;
            previousPosition = transform.position;
            previousPosition.y = 0;
            CurrentState = Boss_01States.Attacking;
            return;
        }
    }

    private void ChaseState()
    {

        //Change state possibilities
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if(_chaseTimerStart + minChaseTime < Time.time && distance <= AttackDistance)
        {

            targetPosition = player.transform.position;
            targetPosition.y = 0;


            _mc.MoveInput = Vector2.zero;

            CurrentState = Boss_01States.PrepareAttack;
            _prepareTimer = Time.time;
            return;
        }

        Vector3 direction = player.transform.position - transform.position;
        Vector2 directionInput = new Vector2(direction.x, direction.z).normalized;
        _mc.MoveInput = directionInput;

    }
}