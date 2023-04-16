using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public static UIManager instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(this);
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        if (!InputManager._inputActions.Minigame.enabled)
            InputManager.ToggleActionMap(InputManager._inputActions.Minigame);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleCanvasGroup(CanvasGroup group)
    {
        if (group.alpha == 0)
            group.alpha = 1;
        else group.alpha = 0;

        group.blocksRaycasts = !group.blocksRaycasts;
        group.interactable = !group.interactable;
    }
}
