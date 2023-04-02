using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Controls")]
    /*
    [SerializeField]private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField]private KeyCode jumpKey = KeyCode.Space;
    [SerializeField]private KeyCode crouchKey = KeyCode.LeftControl;
    [SerializeField]private KeyCode zoomKey = KeyCode.Mouse1;
    [SerializeField]private KeyCode interactKey = KeyCode.E;
    */
    [SerializeField] public InputAssetController input;

    [Header("Functions")]
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canCrouch = true;
    [SerializeField] private bool canHeadBob = true;
    [SerializeField] private bool willSlideOnSlopes = true;
    ///[SerializeField] private bool canZoom = true;
    [SerializeField] private bool canInteract = true;
    [SerializeField] private bool useFootsteps = true;
    [SerializeField] private bool useStamina = true;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private float crouchSpeed = 1.5f;
    [SerializeField] private float sprintSpeed = 6.0f;
    [SerializeField] private float slopeSpeed = 8.0f;
    [SerializeField] private float jumpSpeed = 8.0f;
    [SerializeField] private float gravity = 30.0f;
    
    [Header("Camera Parameters")]
    [SerializeField, Range(1,10)] private float lookSpeedX = 2.0f;
    [SerializeField, Range(1,10)] private float lookSpeedY = 2.0f;
    [SerializeField, Range(1,180)] private float upperLookLimit = 80.0f;
    [SerializeField, Range(1,180)] private float lowerLookLimit = 80.0f;

    [Header("Crouching Parameters")]
    [SerializeField] private float crouchHeight = 0.5f;
    [SerializeField] private float standHeight = 2.0f;
    [SerializeField] private float timeToCrouch = 0.25f;
    [SerializeField] private Vector3 crouchCenter = new Vector3(0f, 0.5f, 0f);
    [SerializeField] private Vector3 standCenter = new Vector3(0f, 0f, 0f);
    [HideInInspector] public bool isCrouching;
    private bool currentlyCrouching;

    [Header("Headbob Parameters")]
    [SerializeField] private float moveBobSpeed = 14f;
    [SerializeField] private float moveBobAmount = 0.05f;
    [SerializeField] private float sprintBobSpeed = 18f;
    [SerializeField] private float sprintBobAmount = 0.1f;
    [SerializeField] private float crouchBobSpeed = 8f;
    [SerializeField] private float crouchBobAmount = 0.025f;
    private float defaultYPos = 0;
    private float cameraTimer;

    [Header("Aim Parameters")]
    [SerializeField] private float timeToZoom = 0.3f;
    [SerializeField] private float zoomFOV = 30.0f;
    private float defualtFOV;
    private Coroutine zoomRoutine;

    [Header("Footstep Parameters")]
    [SerializeField] private float moveStepSpeed = 0.5f;
    [SerializeField] private float crouchStepMultiplier = 1.5f;
    [SerializeField] private float sprintStepMultiplier = 0.6f;
    [SerializeField] private AudioClip[] woodClips = default;
    [SerializeField] private AudioClip[] metalClips = default;
    [SerializeField] private AudioClip[] grassClips = default;
    private float footstepTimer = 0;
    private float GetCurrentOffset => isCrouching ? moveStepSpeed * crouchStepMultiplier : isSprinting ? moveStepSpeed * sprintStepMultiplier : moveStepSpeed;

    [Header("Stamina Parameters")]
    [SerializeField] private float maxStamina = 100;
    [SerializeField] private float staminaUseMultiplier = 5;
    [SerializeField] private float timeBeforeStamRegenStarts = 5;
    [SerializeField] private float staminaValueIncrement = 2;
    [SerializeField] private float staminaTimeIncrement = 0.1f;
    private float currentStamina;
    private Coroutine regeneratingStamina;

    private Vector3 hitPointNormal;
    private bool isSliding
    {
        get
        {
            if (characterController.isGrounded && Physics.Raycast(transform.position, Vector3.down, out RaycastHit slopeHit, 2f))
            {
                hitPointNormal = slopeHit.normal;
                return Vector3.Angle(hitPointNormal, Vector3.up) >= characterController.slopeLimit;
            }
            else
            {
                return false;
            }
        }
    }

    [Header("Interaction")]
    [SerializeField] private Vector3 interactionRayPoint = default;
    [SerializeField] private float interactionDistance = default;
    [SerializeField] private LayerMask interactionLayer = default;
    private InteractableObjects currentInteractableObject;

    private Camera playerCamera;
    private CharacterController characterController;
    [SerializeField] private AudioSource audioSource = default;
    private Vector3 moveDirection;
    [HideInInspector] public Vector2 currentInput;
    private float rotationX = 0;

    public bool canMove {get;private set;} = true;
    private bool isSprinting;
    private bool shouldJump;
    private bool shouldCrouch;

    [HideInInspector] public bool holdingWin;


    private PlayerInput playerInputAction;

    // Start is called before the first frame update
    void Awake()
    {   
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        defaultYPos = playerCamera.transform.position.y;
        defualtFOV = playerCamera.fieldOfView;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentStamina = maxStamina;

        readyToThrow = true;
    }

    void Start()
    {
        // input.MoveEvent += HandleMove;
        // input.LookEvent += HandleLook;
        // input.JumpEvent += HandleJump;
        // input.CrouchEvent += HandleCrouch;
        // input.SprintEvent += HandleSprint;
        // input.SprintCanceledEvent += HandleSprintCancelled;
        // input.InteractEvent += HandleInteraction;
    }

    
    // private void HandleMove(Vector2 dir)
    // {
    //     Debug.Log(dir);
    //     GatherInputs(dir);
    // }

    // private void HandleLook(Vector2 dir)
    // {
    //     Debug.Log(dir);
    //     MouseOperations(dir);
    // }

    // private void HandleInteraction() 
    // {
    //     Debug.Log("Interaction");
    //     HandleInteractionCheck();
    //     HandleInteractionInput();
    // }

    // private void HandleSprint()
    // {
    //     isSprinting = true;
    // }

    // private void HandleSprintCancelled()
    // {
    //     isSprinting = false;
    // }

    // private void HandleCrouch()
    // {
    //     if (!currentlyCrouching && characterController.isGrounded)
    //     {
    //         shouldCrouch = true;
    //     }
    // }

    // private void HandleJump()
    // {
    //     if (characterController.isGrounded)
    //     {
    //         shouldJump = true;
    //     }
    // }
    

    public void HandleMove(InputAction.CallbackContext action)
    {
        var dir = action.ReadValue<Vector2>();
        Debug.Log(dir);
        GatherInputs(dir);
    }

    public void HandleLook(InputAction.CallbackContext action)
    {
        var dir = action.ReadValue<Vector2>();
        Debug.Log(dir);
        MouseOperations(dir);
    }

    public void HandleInteraction(InputAction.CallbackContext action) 
    {

        Debug.Log("Interaction");
        HandleInteractionCheck();
        HandleInteractionInput();
    }

    private void HandleSprint()
    {
        isSprinting = true;
    }

    private void HandleSprintCancelled()
    {
        isSprinting = false;
    }

    private void HandleCrouch()
    {
        if (!currentlyCrouching && characterController.isGrounded)
        {
            shouldCrouch = true;
        }
    }

    private void HandleJump()
    {
        if (characterController.isGrounded)
        {
            shouldJump = true;
        }
    }

    public void HandlePause(InputAction.CallbackContext action)
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentStamina.ToString());
        if (canMove)
        {
            Move();
            Look();

            if (canJump)
            {
                UpdateJump();
            }

            if (canCrouch)
            {
                UpdateCrouch();
            }

            if (canHeadBob)
            {
                HandleHeadBob();
            }

            /*
            if (canZoom)
            {
                HandleAim();
            }
            */

            if (canInteract)
            {
                HandleInteractionCheck();
                HandleInteractionInput();
            }

            if (useFootsteps)
            {
                HandleFootsteps();
            }

            if (useStamina)
            {
                HandleStamina();
            }

            ApplyPhysics();
        }

        /*

        if(Input.GetKeyDown(throwKey) && readyToThrow && totalThrows > 0)
        {
            Throw();
        }

        */
    }

    void FixedUpdate()
    {
        
    }

    private void GatherInputs(Vector2 direction)
    {
        currentInput = new Vector2((isCrouching ? crouchSpeed : isSprinting ? sprintSpeed : moveSpeed) * direction.y, (isCrouching ? crouchSpeed : isSprinting ? sprintSpeed : moveSpeed) * direction.x);
        
    }

    private void Move()
    {
        float moveDirectionY = moveDirection.y;
        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) + (transform.TransformDirection(Vector3.right) * currentInput.y);
        moveDirection.y = moveDirectionY;
    }

    private void MouseOperations(Vector2 direction)
    {
        rotationX -= direction.y * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);

        transform.rotation *= Quaternion.Euler(0, direction.x * lookSpeedX, 0);
    }

    private void Look()
    {
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }

    private void ApplyPhysics()
    {
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        if (willSlideOnSlopes && isSliding)
        {
            moveDirection += new Vector3(hitPointNormal.x, -hitPointNormal.y, hitPointNormal.z) * slopeSpeed;
        }

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void UpdateJump()
    {
        if (shouldJump)
        {
            moveDirection.y = jumpSpeed;
        }
    }

    private void UpdateCrouch()
    {
        if (shouldCrouch)
        {
            StartCoroutine(ToggleCrouch());
        }
    }

    private IEnumerator ToggleCrouch()
    {
        if (isCrouching && Physics.Raycast(playerCamera.transform.position, Vector3.up, 1f))
        {
            yield break;
        }

        currentlyCrouching = true;

        float timeElapsed = 0;
        float targetHeight = isCrouching ? standHeight : crouchHeight;
        float currentHeight = characterController.height;
        Vector3 targetCenter = isCrouching ? standCenter : crouchCenter;
        Vector3 currentCenter = characterController.center;

        while (timeElapsed < timeToCrouch)
        {
            characterController.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / timeToCrouch);
            characterController.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed / timeToCrouch);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        characterController.height = targetHeight;
        characterController.center = targetCenter;

        isCrouching = !isCrouching;

        currentlyCrouching = false;
    }

    private void HandleHeadBob()
    {
        if (!characterController.isGrounded)
        {
            return;
        }

        if (Mathf.Abs(moveDirection.x) > 0.1f || Mathf.Abs(moveDirection.z) > 0.1f)
        {
            cameraTimer += Time.deltaTime * (isCrouching ? crouchBobSpeed : isSprinting ? sprintBobSpeed : moveBobSpeed);
            playerCamera.transform.localPosition = new Vector3(playerCamera.transform.localPosition.x, defaultYPos + Mathf.Sin(cameraTimer) * (isCrouching ? crouchBobAmount : isSprinting ? sprintBobAmount : moveBobAmount), playerCamera.transform.localPosition.z);
        }
    }

    //If we want zoom back, we can do this
    /*
    private void HandleAim()
    {
        if (Input.GetKeyDown(zoomKey))
        {
            if (zoomRoutine != null)
            {
                StopCoroutine(zoomRoutine);
                zoomRoutine = null;
            }

            zoomRoutine = StartCoroutine(ToggleZoom(true));
        }
        if (Input.GetKeyUp(zoomKey))
        {
            if (zoomRoutine != null)
            {
                StopCoroutine(zoomRoutine);
                zoomRoutine = null;
            }

            zoomRoutine = StartCoroutine(ToggleZoom(false));
        }
    }
    */

    private IEnumerator ToggleZoom(bool isEnter)
    {
        float targetFOV = isEnter ? zoomFOV : defualtFOV;
        float startingFOV = playerCamera.fieldOfView;
        float timeElapsed = 0;

        while(timeElapsed < timeToZoom)
        {
            playerCamera.fieldOfView = Mathf.Lerp(startingFOV, targetFOV, timeElapsed / timeToZoom);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        playerCamera.fieldOfView = targetFOV;
        zoomRoutine = null;
    }

    private void HandleInteractionCheck()
    {
        if (Physics.Raycast(playerCamera.ViewportPointToRay(interactionRayPoint), out RaycastHit hit, interactionDistance))
        {
            if (hit.collider.gameObject.layer == 6 && (currentInteractableObject == null || hit.collider.gameObject.GetInstanceID() != currentInteractableObject.GetInstanceID()))
            {
                hit.collider.TryGetComponent(out currentInteractableObject);

                if (currentInteractableObject)
                {
                    currentInteractableObject.OnFocus();
                }
            }
        }
        else if (currentInteractableObject)
        {
            currentInteractableObject.OnLoseFocus();
            currentInteractableObject = null;
        }
    }

    private void HandleInteractionInput()
    {
        if (currentInteractableObject != null && Physics.Raycast(playerCamera.ViewportPointToRay(interactionRayPoint), out RaycastHit hit, interactionDistance, interactionLayer))
        {
            currentInteractableObject.OnInteract();
        }
    }

    private void HandleFootsteps()
    {
        if (!characterController.isGrounded)
        {
            return;
        }
        if (currentInput == Vector2.zero)
        {
            return;
        }

        footstepTimer -= Time.deltaTime;

        if (footstepTimer <= 0)
        {
            if (Physics.Raycast(playerCamera.transform.position, Vector3.down, out RaycastHit hit, 3))
            {
                switch (hit.collider.tag)
                {
                    case "Footsteps/Wood":
                        audioSource.PlayOneShot(woodClips[UnityEngine.Random.Range(0, woodClips.Length - 1)]);
                        break;
                    case "Footsteps/Metal":
                        audioSource.PlayOneShot(metalClips[UnityEngine.Random.Range(0, metalClips.Length - 1)]);
                        break;
                    case "Footsteps/Grass":
                        audioSource.PlayOneShot(grassClips[UnityEngine.Random.Range(0, grassClips.Length - 1)]);
                        break;
                    default:
                        audioSource.PlayOneShot(woodClips[UnityEngine.Random.Range(0, woodClips.Length - 1)]);
                        break;
                }
            }
            footstepTimer = GetCurrentOffset;
        }
    }

    private void HandleStamina()
    {
        if (isSprinting && (currentInput != Vector2.zero))
        {
            if (regeneratingStamina != null)
            {
                StopCoroutine(regeneratingStamina);
                regeneratingStamina = null;
            }
            currentStamina -= staminaUseMultiplier * Time.deltaTime;
            if (currentStamina < 0)
            {
                currentStamina = 0;
            }
            if (currentStamina <= 0)
            {
                canSprint = false;
            }
        }

        if (!isSprinting && currentStamina < maxStamina && regeneratingStamina != null)
        {
            regeneratingStamina = StartCoroutine(RegenerateStamina());
        }
    }

    private IEnumerator RegenerateStamina()
    {
        yield return new WaitForSeconds(timeBeforeStamRegenStarts);

        WaitForSeconds timeToWait = new WaitForSeconds(staminaTimeIncrement);

        while (currentStamina < maxStamina)
        {
            if (currentStamina > 0)
            {
                canSprint = true;
            }
            currentStamina += staminaValueIncrement;

            if (currentStamina > maxStamina)
            {
                currentStamina = maxStamina;
            }
            yield return timeToWait;
        }

        regeneratingStamina = null;
    }

//throwing items

    [Header("References")]
    public Transform cam;
    public Transform attackPoint;
    public GameObject objectToThrow;

    [Header("Settings")]
    public int totalThrows;
    public float throwCooldown;

    [Header("Throwing")]
    //public KeyCode throwKey = KeyCode.Mouse0;
    public float throwForce;
    public float throwUpwardForce;

    bool readyToThrow;

    private void Throw()
    {
        readyToThrow = false;

        //instantiate object to throw
        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);

        //get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        //calculate direction
        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;

        if(Physics.Raycast(cam.position,cam.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        //add force
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;

        //implement cooldown
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }


}
