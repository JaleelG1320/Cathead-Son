using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class BusDriverInteractableScript : InteractableObjects
{
    private bool interacted;
    public GameObject interactPrefab;
    public GameObject levelUI;
    private GameObject interactIcon;
    private CharacterSwap swapReference;
    private CinemachineVirtualCamera cameraReference;
    public override void OnFocus()
    {
        swapReference = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<CharacterSwap>();
        cameraReference = GetComponentInChildren<CinemachineVirtualCamera>();
        CameraSwitcher.Register(cameraReference);
        
    }

    public override void OnInteract()
    {
        interacted = true;
        //turn olma off
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        // spawn ui 
        // interactIcon = Instantiate(interactPrefab, gameObject.transform.position, Quaternion.identity);
        levelUI.SetActive(true);
        levelUI.transform.GetChild(0).GetComponentInChildren<Button>().Select();
        //switch the camera to the level ui 
        CameraSwitcher.SwitchCamera(cameraReference);
        //switch player controls over to the ui 
        InputManager._inputActions.Player.Disable();
        InputManager._inputActions.Minigame.Enable();
        InputManager.ToggleActionMap(InputManager._inputActions.UI);

        
    }

    public override void OnLoseFocus()
    {
        
        
    }

    public void CloseLevelUI(InputAction.CallbackContext obj)
    {
        if (GameObject.Find("BusDriver").activeInHierarchy && interacted)
        {
            if (obj.started)
            {
                Debug.Log("Closing UI");
                //turn olma on
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                //destroy ui object
                //Destroy(interactPrefab, 1.5f);
                levelUI.SetActive(false);
                //switch camera back to player camera
                CameraSwitcher.SwitchCamera(swapReference.gameObject.GetComponentInChildren<CinemachineVirtualCameraBase>());
                //switch controls back to player
                InputManager._inputActions.Minigame.Disable();
                InputManager._inputActions.Player.Enable();
                InputManager.ToggleActionMap(InputManager._inputActions.Player);
                interacted = false;
            }
        }


    }
}
