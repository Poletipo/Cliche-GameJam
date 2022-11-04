using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{


    public float Speed = 5;

    Rigidbody _rb;
    private Vector2 _moveInput;



    Vector3 _velocity = new Vector3();


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
        _velocity = _rb.velocity;



        Vector2 desiredVelocity = MoveInput * Speed;

        _velocity.x = desiredVelocity.x;
        _velocity.z = desiredVelocity.y;

        _rb.velocity = _velocity;




        if(MoveInput.magnitude > 0) {

            Vector3 direction = new Vector3(MoveInput.x, 0, MoveInput.y);

            transform.rotation = Quaternion.LookRotation(direction);


        }


    }
}
