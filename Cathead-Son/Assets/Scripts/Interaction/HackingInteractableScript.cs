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
    //private MeshRenderer _playerMeshReference;
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
    private int currentTerminal = 0; 
    public ParticleSystem finishEffect;
    public GameObject sparkle1;
    public GameObject sparkle2;
    public GameObject sparkle3;
    private GameObject enemyReference;
    private Animator anim;

    public void Awake()
    {
        _minigameCamera = GetComponentInChildren<CinemachineVirtualCameraBase>();
        CameraSwitcher.Register(_minigameCamera.gameObject.GetComponent<CinemachineVirtualCameraBase>());
    }
    public override void OnInteract()
    {
        Debug.Log("Interacted with " + gameObject.name);
        _playerReference = GameObject.Find("tommy");
        anim = _playerReference.GetComponent<Animator>();
        _playerReference = GameObject.FindGameObjectWithTag("CurrentPlayer");
        //_playerMeshReference = _playerReference.GetComponent<MeshRenderer>();
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
        _playerScriptReference.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);

    }

    public void SwitchToPlayer()
    {
        _playerScriptReference.enabled = true;
        anim.Play("Idle", -1, 0f);
        _playerScriptReference.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        InputManager.ToggleActionMap(InputManager._inputActions.Minigame);
        InputManager.ToggleActionMap(InputManager._inputActions.Player);
        CameraSwitcher.SwitchCamera(CameraSwitcher.cameras[CameraSwitcher.cameras.Count - 1]);
        if(triggerObject != null)
            Debug.Log("WOrks");
        if(currentTerminal == 0){
            sparkle1.SetActive(false);
            sparkle2.SetActive(true);
            triggerObject = GameObject.Find("Door2");
            currentTerminal++;
            enemyReference = GameObject.Find("Enemy1");
            enemyReference.SetActive(false);
        }
        else if(currentTerminal == 1){
            sparkle2.SetActive(false);
            sparkle3.SetActive(true);
            triggerObject = GameObject.Find("Door3");
            currentTerminal++;
        }
        else if (currentTerminal == 2){
            sparkle3.SetActive(false);
            Destroy(gameObject);
        }
        gameObject.transform.Translate(-6,0,0);

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
