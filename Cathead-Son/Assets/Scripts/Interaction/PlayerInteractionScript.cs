using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

    [RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
    [RequireComponent(typeof(PlayerInput))]
#endif
public class PlayerInteractionScript : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] private Vector3 interactionRayPoint = default;
    [SerializeField] private float interactionDistance = default;
    [SerializeField] private LayerMask interactionLayer = default;
    [SerializeField] private Camera playerCamera;
    private InteractableObjects currentInteractableObject;
    [HideInInspector] public float publicInteractionDistance;
    private Collider[] interactableObjectsInArea;

    

    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleInteractionCheck();
        HandleInteractionInput();
    }

    private void HandleInteractionCheck()
    {
        interactableObjectsInArea = Physics.OverlapSphere(gameObject.transform.position, interactionDistance, interactionLayer);
        if (interactableObjectsInArea.Length > 0)
        {
            foreach (Collider collider in interactableObjectsInArea)
            {
                if (collider.gameObject.layer == 6 && (currentInteractableObject == null || collider.gameObject.GetInstanceID() != currentInteractableObject.GetInstanceID()))
                {
                    collider.TryGetComponent(out currentInteractableObject);

                    if (currentInteractableObject)
                    {
                            currentInteractableObject.OnFocus();
                    }
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
        foreach (Collider collider in interactableObjectsInArea)
        {
            //needs fix to work with input system used for new game
            /*
            if (_input.interact && currentInteractableObject != null && Vector3.Distance(currentInteractableObject.transform.position, gameObject.transform.position) < 1.7f)
            {
                currentInteractableObject.OnInteract();
            }
            */
        }
            
    }
}
