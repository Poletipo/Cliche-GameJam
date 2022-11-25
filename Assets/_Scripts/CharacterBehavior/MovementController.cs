using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    private bool _isEnabled = true;

    public bool IsEnabled
    {
        get { return _isEnabled; }
        set {
            if (!value)
            {
                MoveInput = Vector2.zero;
                _rb.velocity = Vector3.zero;
            }

            _isEnabled = value; 
        }
    }

    public float MaxSpeed = 5;
    public float Acceleration = 1;

    public float RotationSpeed = 20;

    public Rigidbody _rb;
    private Vector2 _moveInput;

    private float angle = 0;

    public Vector3 Velocity = new Vector3();


    public Vector2 MoveInput
    {
        get { return _moveInput; }
        set { _moveInput = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (IsEnabled)
        {
            HandleMovement();
            HandleRotation();
        }
            HandleKnockback();
    }

    private bool isKnockedback = false;
    public AnimationCurve KnockBackCurve;
    private float knockBackForce = 10f;
    private float knockBackTime = 0.5f;
    private float knockBackStartTime;
    private Vector3 knockbackDirection;

    private void HandleKnockback()
    {
        if (isKnockedback)
        {

            float t = Mathf.Clamp01((Time.time - knockBackStartTime) / knockBackTime);

            if (t >= 1 || (_rb.velocity.sqrMagnitude < 0.2f && t >= (knockBackTime/2)) )
            {
                isKnockedback = false;
                IsEnabled = true;
                return;
            }

            t = KnockBackCurve.Evaluate(t);
            t = 1 - t;

            _rb.velocity = knockbackDirection * t * knockBackForce;
        }

    }

    private void HandleMovement()
    {
        Velocity = _rb.velocity;

        Vector2 desiredVelocity = MoveInput * MaxSpeed;

        Velocity.x = desiredVelocity.x;
        Velocity.z = desiredVelocity.y;

        _rb.velocity = Velocity;
    }

    private void HandleRotation()
    {
        if (MoveInput.magnitude > 0)
        {
            float desiredAngle = Mathf.Rad2Deg * Mathf.Atan2(MoveInput.x, MoveInput.y);

            angle = Mathf.LerpAngle(angle, desiredAngle, RotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        }
    }

    public void KnockBack(Vector3 direction,float force, float time)
    {
        knockBackForce = force;
        knockBackTime = time;

        knockBackStartTime = Time.time;
        knockbackDirection = direction;
        isKnockedback = true;
        IsEnabled = false;
    }


}
