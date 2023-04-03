using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public PlayerInput _playerInput;
    public bool GameIsPaused = false;
    public Button[] Buttons;

    public GameObject pauseMenuUI;

    void Update()
    {

    }

    public void Resume ()
    {
        _playerInput = GameObject.FindGameObjectWithTag("CurrentPlayer").GetComponent<PlayerInput>();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Pause ()
    {
        _playerInput = GameObject.FindGameObjectWithTag("CurrentPlayer").GetComponent<PlayerInput>();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Toggle(InputAction.CallbackContext obj)
    {
        if (obj.started)
        {
            if (GameIsPaused)
            {
                Resume();
                Debug.Log("Player Actions");
                InputManager.ToggleActionMap(InputManager._inputActions.Minigame);
                InputManager.ToggleActionMap(InputManager._inputActions.Player);
            }
            else
            {
                Pause();
                Debug.Log("Minigame Actions");
                InputManager.ToggleActionMap(InputManager._inputActions.Player);
                InputManager.ToggleActionMap(InputManager._inputActions.Minigame);

                Debug.Log("Poop");
            }
        }
    }
}
