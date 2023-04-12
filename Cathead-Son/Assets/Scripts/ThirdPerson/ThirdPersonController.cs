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
    [SerializeField] private InputActionReference movementControl;
    [SerializeField] private InputActionReference lookControl;
    private Vector2 _move;
    private Vector2 _look;
    public Animator anim;

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
    private float rotationSpeed = 3f;

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
            CameraRotation(); //rotates camera
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

    public void DoSwapCharPrev(InputAction.CallbackContext obj)
    {
        if (obj.started)
            CharacterSwapController.SwapCharacterPrev();
    }

    public void DoMove(InputAction.CallbackContext obj)
    {
        _move = obj.ReadValue<Vector2>();
        if(_move != Vector2.zero){
            anim.SetInteger("Walk", 1);
        }
        else {
            anim.SetInteger("Walk", 0);
            Debug.Log("Stopping Workjs");
        }
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

    private void MovementControl()
    {
        Vector2 movement = movementControl.action.ReadValue<Vector2>();
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        move = _playerCamera.transform.forward * move.z + _playerCamera.transform.right * move.x;
        move.y = 0f;
    }

    private void CameraRotation()
    {
        if (_move != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(_move.x, _move.y) * Mathf.Rad2Deg + _playerCamera.transform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }
    }
}
