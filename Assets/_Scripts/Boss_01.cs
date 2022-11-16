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
        Stunned
    }


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


    public Boss_01States currentState = Boss_01States.Idle;


    public GameObject player;

    MovementController _mc;

    private Vector3 attackPosition;
    private Vector3 targetPosition;



    // Start is called before the first frame update
    void Start()
    {
        _mc = GetComponent<MovementController>();
        startTimer = Time.time;
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

            default:
                break;
        }

    }

    private void StuckState()
    {
        if (_stuckTimer + stuckTime < Time.time)
        {
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

        //ax2 + 1;



        Vector3 groundPos = Vector3.Lerp(attackPosition, targetPosition, t);

        groundPos.y = JumpAnimCurve.Evaluate(t);

        //groundPos.y = -Mathf.Pow((t-.5f)*2 ,2) + 1;

        transform.position = groundPos;
    }

    private void IdleState()
    {
        
        if(startTime + startTimer < Time.time)
        {
            currentState = Boss_01States.Chase;
            return;
        }
    }

    private void PrepareAttack()
    {

        if (_prepareTimer + PrepareTime < Time.time)
        {
            startAttackTime = Time.time;
            attackPosition = transform.position;
            attackPosition.y = 0;
            currentState = Boss_01States.Attacking;
            return;
        }
    }

    private void ChaseState()
    {

        //Change state possibilities
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if(distance <= AttackDistance)
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
