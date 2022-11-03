using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{


    Cliche_InputAction _myInput;
    [SerializeField]
    MovementController _mc;


    // Start is called before the first frame update
    void Start()
    {
        _myInput = new Cliche_InputAction();

        _myInput.Player.Move.performed += OnMove;
        _myInput.Player.Move.canceled += OnMoveStop;
        _myInput.Enable();



    }

    private void OnMoveStop(InputAction.CallbackContext obj)
    {
        _mc.MoveInput = Vector2.zero;
    }

    private void OnMove(InputAction.CallbackContext obj)
    {
        _mc.MoveInput = obj.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
