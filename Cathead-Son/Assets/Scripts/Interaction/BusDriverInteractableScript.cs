using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class BusDriverInteractableScript : InteractableObjects
{
    public GameObject interactPrefab;
    public GameObject levelUI;
    private GameObject interactIcon;
    private CharacterSwap swapReference;
    private CinemachineVirtualCamera cameraReference;
    public override void OnFocus()
    {
        swapReference = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<CharacterSwap>();
        cameraReference = GetComponentInChildren<CinemachineVirtualCamera>();
        
    }

    public override void OnInteract()
    {
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

    public void CloseLevelUI()
    {
        //turn olma on
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        //destroy ui object
        //Destroy(interactPrefab, 1.5f);
        levelUI.SetActive(false);
        //switch camera back to player camera
        CameraSwitcher.SwitchCamera(GameObject.FindGameObjectWithTag("PlayerController").GetComponentInChildren<CinemachineVirtualCamera>());
        //switch controls back to player
        InputManager._inputActions.Minigame.Disable();
        InputManager._inputActions.Player.Enable();
        InputManager.ToggleActionMap(InputManager._inputActions.Player);
    }
}
