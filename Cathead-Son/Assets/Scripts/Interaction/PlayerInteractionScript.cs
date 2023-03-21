using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    }

    private void HandleInteractionCheck()
    {
        Debug.Log("Calling the function");
        interactableObjectsInArea = Physics.OverlapSphere(gameObject.transform.position, interactionDistance, interactionLayer);
        if (interactableObjectsInArea.Length > 0)
        {
            foreach (Collider collider in interactableObjectsInArea)
            {
                Debug.Log("Its in the array");
                if (collider.gameObject.layer == 6 && (currentInteractableObject == null || collider.gameObject.GetInstanceID() != currentInteractableObject.GetInstanceID()))
                {
                    collider.TryGetComponent(out currentInteractableObject);

                    if (currentInteractableObject)
                    {
                        Debug.Log("Can see the object");
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

    public void HandleInteractionInput(InputAction.CallbackContext obj)
    {
        foreach (Collider collider in interactableObjectsInArea)
        {
            if (obj.started && currentInteractableObject != null && Vector3.Distance(currentInteractableObject.transform.position, gameObject.transform.position) < 5.7f)
            {
                currentInteractableObject.OnInteract();
            }
        }
            
    }
}
