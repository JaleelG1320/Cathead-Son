using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading;

public class PlayerInteractionScript : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] private Vector3 _interactionRayPoint = default;
    [SerializeField] private float _interactionDistance = default;
    [SerializeField] private LayerMask _interactionLayer = default;
    [SerializeField] private Camera _playerCamera;
    private InteractableObjects _currentInteractableObject;
    private Collider[] _interactableObjectsInArea;
    public Animator anim;
    private IEnumerator coroutine;
    
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
        // Look for interactables within a given radius, store them.
        _interactableObjectsInArea = Physics.OverlapSphere(gameObject.transform.position, _interactionDistance, _interactionLayer);

        if (_interactableObjectsInArea.Length > 0)
        {
            foreach (Collider collider in _interactableObjectsInArea)
            {
                // Check if our object is interactble, and if it exists - is a new object that we've looked at.
                if (collider.gameObject.layer == 6 && (_currentInteractableObject == null || collider.gameObject.GetInstanceID() != _currentInteractableObject.GetInstanceID()))
                {
                    // Set as our new current interactable.
                    collider.TryGetComponent(out _currentInteractableObject);

                    if (_currentInteractableObject)
                    {
                        _currentInteractableObject.OnFocus(); // Focus on it.
                    }
                }
            }
        }
        else if (_currentInteractableObject) // We didnt not find any interactables, lose focus on our current one.
        {
            _currentInteractableObject.OnLoseFocus();
            _currentInteractableObject = null;
        }

                
    }

    public void HandleInteractionInput(InputAction.CallbackContext obj)
    {
        foreach (Collider collider in _interactableObjectsInArea)
        {
            if (obj.started && _currentInteractableObject != null && Vector3.Distance(_currentInteractableObject.transform.position, gameObject.transform.position) < 5.7f)
            {
                anim.Play("Interact", -1, 0f);
                Invoke(nameof(Delay), 1.5f);
            }
        }
            
    }
    void Delay()
    {
        _currentInteractableObject.OnInteract();
    }
}
