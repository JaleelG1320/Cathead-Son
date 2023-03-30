using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine.InputSystem;

public class HackingInteractableScript : InteractableObjects
{
    [SerializeField] private RenderTexture _minigameRenderer;
    [SerializeField] private GameObject _playerReference;
    [SerializeField] private ThirdPersonController _playerScriptReference;
    [SerializeField] private PlayerInput _playerInputReference;
    private Scene _minigameScene;
    public CinemachineVirtualCamera _minigameCamera;
    private GameObject[] _minigameObjects;
    [HideInInspector] public bool isPlayingMinigame;
    

    [Header("Camera Properties")]

    public GameObject playerCameraReference;
    public Transform tvPosition;
    public Transform playerCameraPosition;
    [Range(1,3)]
    public float timeToMoveCamera;
    private bool currentlyMoving = false;
    private CinemachineFreeLook _freeLook;
    private CinemachineInputProvider _freeLookInput;

    public GameObject[] Cameras;

    public void ActivateCamera(int index)
    {
        for (int i = 0; i < Cameras.Length; i++)
        {
            if (i == index)
            {
                Cameras[i].SetActive(true);
            }
            else
            {
                Cameras[i].SetActive(false);
            }
        }
    }


    public override void OnInteract()
    {
        Debug.Log("Interacted with " + gameObject.name);
        _playerReference = GameObject.FindGameObjectWithTag("CurrentPlayer");
        _playerScriptReference = _playerReference.GetComponent<ThirdPersonController>();
        _playerInputReference = _playerReference.GetComponent<PlayerInput>();


        /*
        //gets the minigame scene at the buildindex for minigame scene
        _minigameScene = SceneManager.GetSceneByBuildIndex(1);
        //loads minigame scene, while keeping our current scene open
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        //gets list of all game objects in the scene
        _minigameObjects = _minigameScene.GetRootGameObjects();
        //finds main camera in scene
        foreach (GameObject minigameObject in _minigameObjects)
        {
            if (minigameObject.TryGetComponent<Camera>(out Camera component))
            {
                _minigameCamera = component;
            }
        }
        //sets render texture to camera of minigame scene
        _minigameCamera.targetTexture = _minigameRenderer;
        //gets the cinemachine free look component
        _freeLook = playerCameraReference.GetComponent<CinemachineFreeLook>();
        //gets the cinemachine free look component
        _freeLookInput = playerCameraReference.GetComponent<CinemachineInputProvider>();
        //tells camera to render
        _minigameCamera.Render(); 
        //moves current camera to focus on texture renderer
        if (!currentlyMoving)
        {
            StartCoroutine(ToggleCameraView());
        }
        //set inputs to work for ui instead of player controller

        //store current player camera position in variable
        playerCameraPosition.position = playerCameraReference.transform.position;
        */

        //ActivateCamera(1);
        _playerScriptReference.enabled = false;
        InputManager.ToggleActionMap(InputManager._inputActions.Minigame);
        CameraSwitcher.SwitchCamera(_minigameCamera);
        Debug.Log(InputManager._inputActions.ToString());


    }
    public override void OnFocus()
    {
        Debug.Log("Focused on " + gameObject.name);

    }
    public override void OnLoseFocus()
    {
        Debug.Log("Lost Focus on " + gameObject.name);
    }

    private IEnumerator ToggleCameraView()
    {
        Debug.Log("Moving the Camera");
        currentlyMoving = true;


        //set look at and follow to hacking terminal
        //_freeLook.m_Follow = gameObject.transform;
        //_freeLook.m_LookAt = gameObject.transform;

        //turn off input provider
        //_freeLookInput.enabled = false;

        float timeElapsed = 0;
        Vector3 targetPosition = isPlayingMinigame ? tvPosition.position : playerCameraPosition.position;
        Vector3 currentPosition = playerCameraReference.transform.position;

        while (timeElapsed < timeToMoveCamera)
        {
            
            playerCameraReference.transform.position = Vector3.Lerp(currentPosition, targetPosition, timeElapsed / timeToMoveCamera);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        playerCameraReference.transform.position = targetPosition;

        isPlayingMinigame = !isPlayingMinigame;
        

        currentlyMoving = false;
    }

}
