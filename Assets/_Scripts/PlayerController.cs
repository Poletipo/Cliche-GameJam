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
    [SerializeField]
    IHitter hitter;

    [SerializeField]
    PlayerAnimCtrl playerAnimCtrl;


    // Start is called before the first frame update
    void Start()
    {
        _myInput = new Cliche_InputAction();

        _myInput.Player.Move.performed += OnMove;
        _myInput.Player.Move.canceled += OnMoveStop;

        _myInput.Player.Fire.performed += OnAttack;
        _myInput.Enable();
    }

    private void OnAttack(InputAction.CallbackContext obj)
    {
        playerAnimCtrl.AttackAnim();
    }

    public void ActivateHitter()
    {
        hitter.Activate();
        _mc.IsEnabled = false;
    }
    public void DeactivateHitter()
    {
        hitter.Deactivate();
        _mc.IsEnabled = true;
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
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        float movementVelocity = new Vector2(_mc.Velocity.x, _mc.Velocity.z).magnitude / _mc.MaxSpeed;
        playerAnimCtrl.MovementVelocity = movementVelocity;
    }
}
