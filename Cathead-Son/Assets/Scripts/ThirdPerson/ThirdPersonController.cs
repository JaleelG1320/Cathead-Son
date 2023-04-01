using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class ThirdPersonController : MonoBehaviour
{
    // Input Fields
    private PlayerInput _playerInput;
    private Vector2 _move;
    private Vector2 _look;

    public static CharacterSwap CharacterSwapController;

    // Movement Fields
    private Rigidbody _playerRB;
    [SerializeField]
    private float _movementForce = 1f;
    [HideInInspector]  public Vector3 ForceDirection = Vector3.zero;

    [SerializeField]
    private Camera _playerCamera;

    public bool IsHiding;
    [SerializeField] private HackingInteractableScript _hackingTerminalReference;
    [HideInInspector] public GameObject CurrentPlayer;

    public List<GameObject> gameObjects;
    public bool hasHackingTerminal;
    private void Awake()
    {
        _playerRB = this.GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();

        if (CharacterSwapController == null)
            CharacterSwapController = GetComponentInParent<CharacterSwap>();
    }

    private void OnEnable()
    {
        _playerInput.enabled = true;
    }

    private void OnDisable()
    {
        _playerInput.enabled = false;
    }

    private void FixedUpdate()
    {
        if (_hackingTerminalReference == null)
        {
            // Control player movement.
            ForceDirection += _move.x * GetCameraRight(_playerCamera) * _movementForce;
            ForceDirection += _move.y * GetCameraForward(_playerCamera) * _movementForce;

            _playerRB.AddForce(ForceDirection, ForceMode.Impulse);
            ForceDirection = Vector3.zero;

            LookAt(); // If there is a hacking terminal
        }
        else if(!_hackingTerminalReference.IsPlayingMinigame) // and the player is not hacking.
        {
            // Control player movement.
            ForceDirection += _move.x * GetCameraRight(_playerCamera) * _movementForce;
            ForceDirection += _move.y * GetCameraForward(_playerCamera) * _movementForce;

            _playerRB.AddForce(ForceDirection, ForceMode.Impulse);
            ForceDirection = Vector3.zero;

            LookAt(); // If there is a hacking terminal
        }

        
    }

    // Returns the Camera's Normalized Forward Vector.
    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }
    // Returns the Camera's Normalized Right Vector.
    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    public void DoInteract(InputAction.CallbackContext obj)
    {
        // Interact with whatever that interactable is, but only if it exists / is not null.
        closestInteractable?.Interact();

        if (closestInteractable is not null)
            isHiding = !isHiding;
    }
        
    public void DoSwapCharPrev(InputAction.CallbackContext obj)
    {
        if (obj.started)
            CharacterSwapController.SwapCharacterPrev();
    }

    public void OnPause(InputAction.CallbackContext obj)
    {
        if (obj.started)
            Application.Quit();
    }

    public void DoMove(InputAction.CallbackContext obj)
    {
        _move = obj.ReadValue<Vector2>();
    }

    public void DoLook(InputAction.CallbackContext obj)
    {
        _look = obj.ReadValue<Vector2>();
    }

    public void LookAt()
    {
        Vector3 direction = _playerRB.velocity;
        direction.y = 0f;

        if (_move.sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
            this._playerRB.rotation = Quaternion.LookRotation(direction, Vector3.up);
        else
            _playerRB.angularVelocity = Vector3.zero;
    }
}
