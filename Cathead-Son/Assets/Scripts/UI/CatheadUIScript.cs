using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatheadUIScript : MonoBehaviour
{
    public GameObject optionsUI;
    public GameObject startMenuUI;
    public GameObject controlUI;
    public GameObject creditsUI;

    [Header("Level Selection")]
    public GameObject level1CompleteUI;
    public GameObject level2CompleteUI;
    public GameObject level3CompleteUI;


    public void GoToInfoFirstLevel()
    {
        if (GameManager.instance.musueumLevel.isCompleted)
        {
            return;
        }
        GameManager.instance.GoToInfoMusueumLevel();
    }

    public void GoToInfoSecondLevel()
    {
        if (GameManager.instance.studioLevel.isCompleted)
        {
            return;
        }
        GameManager.instance.GoToInfoStudioLevel();
    }

    public void GoToInfoThirdLevel()
    {
        if (GameManager.instance.officeLevel.isCompleted)
        {
            return;
        }
        GameManager.instance.GoToInfoOfficeLevel();
    }


    public void GoToFirstLevel()
    {
        if (GameManager.instance.musueumLevel.isCompleted)
        {
            return;
        }
        GameManager.instance.GoToFirstLevel();
    }

    public void GoToSecondLevel()
    {
        if (GameManager.instance.studioLevel.isCompleted)
        {
            return;
        }
        GameManager.instance.GoToSecondLevel();
    }

    public void GoToThirdLevel()
    {
        if (GameManager.instance.officeLevel.isCompleted)
        {
            return;
        }
        GameManager.instance.GoToThirdLevel();
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

    public void OptionsSelect()
    {

        optionsUI.SetActive(true);
        startMenuUI.SetActive(false);

        Button optionsBackButton = optionsUI.GetComponentInChildren<Button>();
        optionsBackButton.Select();
    }

    public void CreditsSelect()
    {

        creditsUI.SetActive(true);
        startMenuUI.SetActive(false);

        Button creditsBackButton = creditsUI.GetComponentInChildren<Button>();
        creditsBackButton.Select();
    }

    public void ControlSelect()
    {

        controlUI.SetActive(true);
        startMenuUI.SetActive(false);

        Button controlBackButton = controlUI.GetComponentInChildren<Button>();
        controlBackButton.Select();
    }

    public void OptionsBackSelect()
    {
        optionsUI.SetActive(false);
        startMenuUI.SetActive(true);

        GameManager.instance.GoToMainMenu();
    }

    public void ControlsBackSelect()
    {
        optionsUI.SetActive(false);
        startMenuUI.SetActive(true);

        GameManager.instance.GoToMainMenu();
    }

    public void CreditsBackSelect()
    {
        optionsUI.SetActive(false);
        startMenuUI.SetActive(true);

        GameManager.instance.GoToMainMenu();
    }
}
