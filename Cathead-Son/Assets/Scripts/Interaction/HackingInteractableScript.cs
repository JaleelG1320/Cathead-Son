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
    [HideInInspector] public bool IsPlayingMinigame;
    

    [Header("Camera Properties")]

    public GameObject PlayerCameraReference;
    public Transform TV_TF;
    public Transform PlayerCameraTF;
    [Range(1,3)]
    public float TimeToMoveCamera;
    private bool _currentlyMoving = false;
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

        SwitchToMinigame();
    }

    private void SwitchToMinigame()
    {
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
}
