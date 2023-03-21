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

    Interactable closestInteractable;
    private bool isHiding;
    private SphereCollider interactionSphere;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        interactionSphere = GetComponent<SphereCollider>();

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
        // Interact with whatever that interactable is, but only if it exists / is not null.
        closestInteractable?.Interact();

        if (closestInteractable is not null)
            isHiding = !isHiding;
    }
        
    public void DoSwapCharPrev(InputAction.CallbackContext obj)
    {
        if (obj.started)
            swap.SwapCharacterPrev();
    }

    public void OnPause(InputAction.CallbackContext obj)
    {
        if (obj.started)
            Application.Quit();
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
    private void SetClosestInteractable()
    {
        Interactable closest = null;
        float closestDistance = 0f;

        foreach (Interactable item in Interactable.interactables)
        {
            float newDistance = Vector2.Distance(transform.position, item.transform.position);
            newDistance = newDistance <= interactionSphere.radius ? newDistance : -1f;

            if (newDistance == -1) continue;

            if (closest == null || newDistance < closestDistance)
            {
                closest = item;
                closestDistance = newDistance;
            }
        }

        closestInteractable = closest;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Interactable>(out Interactable interactable))
        {
            ToggleOutLineOfClosestHidingSpot(false);
            Interactable.interactables.Add(interactable);
            SetClosestInteractable();
            ToggleOutLineOfClosestHidingSpot(true);
            Debug.Log(closestInteractable);
            Debug.Log(Interactable.interactables.Count);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Interactable>(out Interactable interactable)
            && Interactable.interactables.Contains(interactable))
        {
            ToggleOutLineOfClosestHidingSpot(false);
            Interactable.interactables.Remove(interactable);
            SetClosestInteractable();
            ToggleOutLineOfClosestHidingSpot(true);
            Debug.Log(closestInteractable);
            Debug.Log(Interactable.interactables.Count);

        }
    }

    public void ToggleOutLineOfClosestHidingSpot(bool toggle = false)
    {
        HidingSpot hidingSpot = closestInteractable?.GetComponent<HidingSpot>();
        if (hidingSpot is not null)
        {
            //hidingSpot.outline.enabled = toggle;
        }
    }
}
