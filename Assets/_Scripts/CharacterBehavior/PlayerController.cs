using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    public enum PlayerState {
        Alive,
        Dead,
        InCutscene
    }

    public Action OnPlayerStateChanged;
    public Action OnKeyCountChanged;


    [SerializeField] MovementController _mc;
    [SerializeField] IHitter hitter;
    [SerializeField] Interacter _interacter;
    [SerializeField] PlayerAnimCtrl playerAnimCtrl;

    [SerializeField] private Health _health;
    [SerializeField] private GameObject DeadSpawn;
    [SerializeField] private Renderer _meshRenderer;
    [SerializeField] private Collider _collider;

    public AudioClip DeadSFX;
    public AudioClip HurtSFX;

    private Cliche_InputAction _myInput;

    PlayerState _currentState = PlayerState.Alive;

    public PlayerState CurrentState {
        get { return _currentState; }
        set {
            _currentState = value;
            OnPlayerStateChanged?.Invoke();
        }
    }

    private int _keyCount = 0;
    public int Keycount {
        get { return _keyCount; }
        set {
            _keyCount = value;
            OnKeyCountChanged?.Invoke();
        }
    }

    void Start() {
        _myInput = new Cliche_InputAction();

        _myInput.Player.Move.performed += OnMove;
        _myInput.Player.Move.canceled += OnMoveStop;

        _myInput.Player.Fire.performed += OnAttack;
        _myInput.Player.Interact.performed += OnInteract;

        _myInput.Player.Pause.performed += OnPause;

        _myInput.Enable();

        _health.OnHurt += OnHurt;
        _health.OnDeath += OnDeath;
    }

    void Update() {
        UpdateAnimation();
    }

    private void OnDestroy() {
        _myInput.Player.Move.performed -= OnMove;
        _myInput.Player.Move.canceled -= OnMoveStop;

        _myInput.Player.Fire.performed -= OnAttack;
        _myInput.Player.Interact.performed -= OnInteract;

        _myInput.Player.Pause.performed -= OnPause;
    }

    private void OnPause(InputAction.CallbackContext obj) {
        if (obj.performed) {
            GameManager.Instance.UI.Pause();
        }
    }

    private void OnDeath() {
        playerAnimCtrl.StopAnim();
        AudioManager.Instance.PlayAudio(DeadSFX, transform.position);
        _meshRenderer.enabled = false;
        _mc.IsEnabled = false;
        _mc.enabled = false;
        _collider.enabled = false;
        Instantiate(DeadSpawn, transform.position, transform.rotation);
        CurrentState = PlayerState.Dead;
    }

    private void OnHurt() {
        playerAnimCtrl.HurtAnim();
        AudioManager.Instance.PlayAudio(HurtSFX, transform.position);
    }

    private void OnInteract(InputAction.CallbackContext obj) {
        if (_interacter.InteractInRange) {
            _interacter.Interactable.Interact(this);
        }
    }

    private void OnAttack(InputAction.CallbackContext obj) {
        playerAnimCtrl.AttackAnim();
    }

    public void ActivateHitter() {
        hitter.Activate();
        _mc.IsEnabled = false;
    }
    public void DeactivateHitter() {
        hitter.Deactivate();
        _mc.IsEnabled = true;
    }

    private void OnMoveStop(InputAction.CallbackContext obj) {
        _mc.MoveInput = Vector2.zero;
    }

    private void OnMove(InputAction.CallbackContext obj) {
        _mc.MoveInput = obj.ReadValue<Vector2>();
    }

    private void UpdateAnimation() {
        float movementVelocity = new Vector2(_mc.Velocity.x, _mc.Velocity.z).magnitude / _mc.MaxSpeed;
        playerAnimCtrl.MovementVelocity = movementVelocity;
    }
}
