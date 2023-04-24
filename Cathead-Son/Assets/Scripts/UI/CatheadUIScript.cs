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

        GameManager.instance.GoToMusuemLevel();
    }

    public void GoToSecondLevel()
    {
        GameManager.instance.GoToStudioLevel();
    }

    public void GoToThirdLevel()
    {
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
