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


    public void GoToFirstLevel()
    {
        GameManager.instance.GoToFirstLevel();
    }

    public void GoToSecondLevel()
    {
        GameManager.instance.GoToSecondLevel();
    }

    public void GoToThirdLevel()
    {
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

        Button creditsBackButton = optionsUI.GetComponentInChildren<Button>();
        creditsBackButton.Select();
    }
}
