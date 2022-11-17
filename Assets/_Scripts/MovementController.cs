using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    public bool IsEnabled = true;

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

    public void KnockBack(Vector3 direction,float force)
    {
        _rb.velocity = direction * force;
    }


}
