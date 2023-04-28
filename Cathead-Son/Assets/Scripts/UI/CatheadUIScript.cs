using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatheadUIScript : MonoBehaviour
{
    public CanvasGroup optionsUI;
    public CanvasGroup startMenuUI;
    public CanvasGroup controlUI;
    public CanvasGroup creditsUI;

    [Header("Level Selection")]
    public GameObject level1CompleteUI;
    public GameObject level2CompleteUI;
    public GameObject level3CompleteUI;

    public void StartGame()
    {
        GameManager.instance.StartGame();
    }

    public void GoToFirstLevel()
    {
        if (GameManager.instance.musueumLevel.isCompleted)
        {
            return;
        }
        GameManager.instance.GoToMusuemLevel();
    }

    public void GoToSecondLevel()
    {
        if (GameManager.instance.studioLevel.isCompleted)
        {
            return;
        }
        GameManager.instance.GoToStudioLevel();
    }

    public void GoToThirdLevel()
    {
        if (GameManager.instance.officeLevel.isCompleted)
        {
            return;
        }
        GameManager.instance.GoToOfficeLevel();
    }

    public void GoToTheHub()
    {
        GameManager.instance.GoToTheHub();
    }

    public void GoToMainMenu()
    {
        GameManager.instance.GoToMainMenu();
    }

    public void QuitGame()
    {
        GameManager.instance.QuitGame();
    }

    private void Start()
    {


        //checks if player has completed level, if so, put paw print over level
        if (GameManager.instance.currentLevel.sceneName == "HubLevel")
        {
            if (GameManager.instance.musueumLevel.isCompleted)
            {
                level1CompleteUI.SetActive(true);
            }
            else
            {
                level1CompleteUI.SetActive(false);
            }
            if (GameManager.instance.studioLevel.isCompleted)
            {
                level2CompleteUI.SetActive(true);
            }
            else
            {
                level2CompleteUI.SetActive(false);
            }
            if (GameManager.instance.officeLevel.isCompleted)
            {
                level3CompleteUI.SetActive(true);
            }
            else
            {
                level3CompleteUI.SetActive(false);
            }
        }

    }

    public void ToggleCanvasGroup(CanvasGroup group)
    {
        if (group.alpha == 0)
        {
            group.GetComponentInChildren<Button>().Select();
            group.alpha = 1;
        }
        else group.alpha = 0;

        group.blocksRaycasts = !group.blocksRaycasts;
        group.interactable = !group.interactable;
    }
}
