using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        Alive,
        Dead,
        InCutscene
    }

    public Action OnPlayerStateChanged;

    PlayerState _currentState = PlayerState.Alive;

    public PlayerState CurrentState
    {
        get { return _currentState; }
        set { 
            _currentState = value;
            OnPlayerStateChanged?.Invoke();
        }
    }

    Cliche_InputAction _myInput;
    [SerializeField]
    MovementController _mc;
    [SerializeField]
    IHitter hitter;
    [SerializeField]
    Interacter _interacter;

    [SerializeField]
    PlayerAnimCtrl playerAnimCtrl;
    public int keycount = 0;
    [SerializeField]
    private Health _health;

    [SerializeField]
    private GameObject DeadSpawn;
    [SerializeField]
    private Renderer _meshRenderer;
    [SerializeField]
    private Collider _collider;


    // Start is called before the first frame update
    void Start()
    {
        _myInput = new Cliche_InputAction();

        _myInput.Player.Move.performed += OnMove;
        _myInput.Player.Move.canceled += OnMoveStop;

        _myInput.Player.Fire.performed += OnAttack;
        _myInput.Player.Interact.performed += OnInteract;

        _myInput.Enable();

        _health.OnHurt += OnHurt;
        _health.OnDeath += OnDeath;
    }

    private void OnDeath()
    {
        _meshRenderer.enabled = false;
        _mc.IsEnabled = false;
        _mc.enabled = false;
        _collider.enabled = false;
        Instantiate(DeadSpawn, transform.position, transform.rotation);
        CurrentState = PlayerState.Dead;
    }

    private void OnHurt()
    {
        //
    }

    private void OnInteract(InputAction.CallbackContext obj)
    {
        if ( _interacter.InteractInRange)
        {
            Debug.Log("Interact");
            _interacter.Interactable.Interact(this);
        }
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
