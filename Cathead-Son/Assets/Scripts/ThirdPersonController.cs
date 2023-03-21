using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ThirdPersonController : MonoBehaviour
{
    // Input Fields
    private PlayerInput playerInput;
    private Vector2 move;

    public static CharacterSwap swap;

    // Movement Fields
    private Rigidbody rb;
    [SerializeField]
    private float movementForce = 1f;
    private Vector3 forceDirection = Vector3.zero;

    [SerializeField]
    private Camera playerCamera;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        if (swap == null)
            swap = GetComponentInParent<CharacterSwap>();

    }

    private void OnEnable()
    {
        playerInput.enabled = true;
    }

    private void OnDisable()
    {
        playerInput.enabled = false;
    }

    private void FixedUpdate()
    {
        
        forceDirection += move.x * GetCameraRight(playerCamera) * movementForce;
        forceDirection += move.y * GetCameraForward(playerCamera) * movementForce;

        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;

        LookAt();
    }
    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    public void DoInteract(InputAction.CallbackContext obj)
    {
    }

    public void DoSwapCharNext(InputAction.CallbackContext obj)
    {
        if (obj.started)
            swap.SwapCharacterNext();
    }

    public void DoSwapCharPrev(InputAction.CallbackContext obj)
    {
        if (obj.started)
            swap.SwapCharacterPrev();
    }

    public void DoMove(InputAction.CallbackContext obj)
    {
        move = obj.ReadValue<Vector2>();
    }

    public void LookAt()
    {
        Vector3 direction = rb.velocity;
        direction.y = 0f;

        if (move.sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
            this.rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        else
            rb.angularVelocity = Vector3.zero;
    }

}
