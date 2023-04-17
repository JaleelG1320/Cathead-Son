using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
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
}
