using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine.InputSystem;

public class HackingInteractableScript : InteractableObjects
{
    private RenderTexture _minigameRenderer;
    private GameObject _playerReference;
    private ThirdPersonController _playerScriptReference;
    private PlayerInput _playerInputReference;
    private Scene _minigameScene;
    private CinemachineVirtualCameraBase _minigameCamera;
    [HideInInspector] public bool IsPlayingMinigame;

    [Header("Camera Properties")]
    [Range(1,3)]
    public float TimeToMoveCamera;
    private bool _currentlyMoving = false;
    private CinemachineFreeLook _freeLook;
    private CinemachineInputProvider _freeLookInput;
    //public GameObject[] Terminal;
    private int currentTerminal = 0; 

    public void Awake()
    {
        _minigameCamera = GetComponentInChildren<CinemachineVirtualCameraBase>();
        CameraSwitcher.Register(_minigameCamera.gameObject.GetComponent<CinemachineVirtualCameraBase>());
    }
    public override void OnInteract()
    {
        Debug.Log("Interacted with " + gameObject.name);
        _playerReference = GameObject.FindGameObjectWithTag("CurrentPlayer");
        _playerScriptReference = _playerReference.GetComponent<ThirdPersonController>();
        _playerInputReference = _playerReference.GetComponent<PlayerInput>();
        SwitchToMinigame();
    }

    private void SwitchToMinigame()
    {
        _playerScriptReference.enabled = false;
        InputManager.ToggleActionMap(InputManager._inputActions.Player);
        InputManager.ToggleActionMap(InputManager._inputActions.Minigame);
        CameraSwitcher.SwitchCamera(_minigameCamera);
        Invoke(nameof(StartMinigame), TimeToMoveCamera + 1f);
    }

    public void SwitchToPlayer()
    {
        _playerScriptReference.enabled = true;
        InputManager.ToggleActionMap(InputManager._inputActions.Minigame);
        InputManager.ToggleActionMap(InputManager._inputActions.Player);
        CameraSwitcher.SwitchCamera(CameraSwitcher.cameras[CameraSwitcher.cameras.Count - 1]);
        if(triggerObject != null)
            Debug.Log("WOrks");
        if(currentTerminal == 0){
            triggerObject = GameObject.Find("Door2");
            currentTerminal++;
        }
        else if(currentTerminal == 1){
            triggerObject = GameObject.Find("Door3");
            currentTerminal++;
        }
        else if (currentTerminal == 2){
            Destroy(gameObject);
        }
        gameObject.transform.Translate(-6,0,0);

        /*if(currentTerminal < 2){
            Terminal[currentTerminal].SetActive(false);
            currentTerminal++;
            Terminal[currentTerminal].SetActive(true);
        }*/
    }

    private void StartMinigame()
    {
        gameObject.GetComponent<MinigameScript>().enabled = true;
    }

    public override void OnFocus()
    {
        Debug.Log("Focused on " + gameObject.name);

    }
    public override void OnLoseFocus()
    {
        Debug.Log("Lost Focus on " + gameObject.name);
    }
}
