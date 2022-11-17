using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_01 : MonoBehaviour
{

    public enum Boss_01States
    {
        Idle,
        Chase,
        PrepareAttack,
        Attacking,
        Stuck,
        Stunned,
        Hit,
        Dead
    }


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

    float startTime = 1;
    float startTimer = 0;

    public AnimationCurve KnockBackCurve;
    public float knockBackDistance = 1.5f;
    public float knockBackTime = 0.5f;
    private float knockBackStartTime;


    public Boss_01States currentState = Boss_01States.Idle;
    private Vector3 knockBackDirection;
    public GameObject player;
    [SerializeField]
    private Health _health;

    MovementController _mc;

    private Vector3 previousPosition;
    private Vector3 targetPosition;



    // Start is called before the first frame update
    void Start()
    {
        _mc = GetComponent<MovementController>();
        startTimer = Time.time;


        _health.OnDeath += OnDeath;
        _health.OnHurt += OnHurt;

    }

    private void OnDeath()
    {
        Destroy(gameObject);
    }

    private void OnHurt()
    {
        currentState = Boss_01States.Hit;
        knockBackDirection = (transform.position - player.transform.position).normalized;

        previousPosition = transform.position;

        targetPosition = previousPosition + (knockBackDirection * knockBackDistance);
        knockBackStartTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
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
            case Boss_01States.Hit:
                HitState();
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

    private void HitState()
    {

        float t = Mathf.Clamp01((Time.time - knockBackStartTime) / knockBackTime);


        if (t >= 1)
        {
            startTimer = Time.time;
            currentState = Boss_01States.Idle;
            return;
        }

        t = KnockBackCurve.Evaluate(t);

        Vector3 pos = Vector3.Lerp(previousPosition, targetPosition, t);
        _mc._rb.MovePosition(pos);
    }

    private void StuckState()
    {
        if (_stuckTimer + stuckTime < Time.time)
        {
            _chaseTimerStart = Time.time;
            currentState = Boss_01States.Chase;
            return;
        }
    }

    private void AttackState()
    {

        float t = Mathf.Clamp01((Time.time - startAttackTime) / loungeAttackTime);

        if(t >= 1)
        {
            _stuckTimer = Time.time;
            currentState = Boss_01States.Stuck;
            return;
        }

        Vector3 groundPos = Vector3.Lerp(previousPosition, targetPosition, t);
        groundPos.y = JumpAnimCurve.Evaluate(t);

        //groundPos.y = -Mathf.Pow((t-.5f)*2 ,2) + 1;

        transform.position = groundPos;
    }

    private void IdleState()
    {
        
        if(startTime + startTimer < Time.time)
        {
            currentState = Boss_01States.Chase;
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
            currentState = Boss_01States.Attacking;
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

            currentState = Boss_01States.PrepareAttack;
            _prepareTimer = Time.time;
            return;
        }





        Vector3 direction = player.transform.position - transform.position;
        Vector2 directionInput = new Vector2(direction.x, direction.z).normalized;
        _mc.MoveInput = directionInput;

    }
}
