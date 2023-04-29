using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager :  MonoBehaviour
{
    [Header("Level References")]
    public Level hubLevel;
    public Level musueumLevel;
    public Level studioLevel;
    public Level officeLevel;
    public Level tutorialLevel;
    public Level win_loseLevel;
    public Level mainMenuLevel;
    public Level infoMusuem;
    public Level infoStudio;
    public Level infoOffice;
    

    [Header("Game Settings")]
    public int volumeSettings;
    public bool vSync;
    public bool bnwActive = false;
    public Level currentLevel;

    [Header("Objective Lists")]
    private List<string> hubObjectives = new List<string>();
    private List<string> musueumObjectives = new List<string>();
    private List<string> studioObjectives = new List<string>();
    private List<string> officeObjectives = new List<string>();

    [Header("Script References")]
    [SerializeField] private PostProcessingManager _processingReference;
    [SerializeField] private CharacterSwap _swapReference;

    [Header("Level List")]
    [SerializeField] private List<Level> levelList = new List<Level>();

    [Header("Shader List")]
    public Shader _grayScaleShader;
    public Shader _circleWipeShader;
    private Material _grayScaleMaterial;
    private Material _circleWipeMaterial;

    public static GameManager instance; //reference to game manager script
    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            //_grayScaleMaterial = new Material(_grayScaleShader);
            //_circleWipeMaterial = new Material(_circleWipeShader);

            levelList.Add(hubLevel);
            levelList.Add(musueumLevel);
            levelList.Add(studioLevel);
            levelList.Add(officeLevel);
            levelList.Add(tutorialLevel);
            levelList.Add(win_loseLevel);
            levelList.Add(mainMenuLevel);
            levelList.Add(infoMusuem);
            levelList.Add(infoStudio);
            levelList.Add(infoOffice);

            //Cursor.lockState = CursorLockMode.Locked;

            DontDestroyOnLoad(this.gameObject);  //makes sure object isnt destroyed when loading levels
        } 
        else 
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.currentLevel = GetActiveLevel();
    }

    //controls what happens when the player wins a level
    public void OnLevelComplete()
    {
        // set current level to be completed
        currentLevel.isCompleted = true;
        StartCoroutine(Wait());
        // take player back to hub level
        if (GameManager.instance.officeLevel.isCompleted &&
           GameManager.instance.studioLevel.isCompleted &&
           GameManager.instance.musueumLevel.isCompleted)
        {
            GameManager.instance.GoToWinScreen();
            return;
        }

        SwitchLevel(this.currentLevel, this.hubLevel);
        StopCoroutine(Wait());
    }

    public void OnLevelFailed()
    {
        // set current level to be incomplete
        this.currentLevel.isCompleted = false;
        // take player back to hub level
        SwitchLevel(this.currentLevel, this.hubLevel);
    }

    public void SwitchLevel(Level _currentLevel, Level _targetLevel)
    {
        Debug.Log("Switching Level");
        //set current level to not active 
        //this.currentLevel.isActive = false;
        //change current level to target level
        this.currentLevel = _targetLevel;
        //set target level to active 
        this.currentLevel.isActive = true;
        //set loading screen to be active
        //this.loadingScreen.SetActive(true);
        //start corotuine to display tips
        //DisplayLoadInformation(_targetLevel);

        //change scene one all tips have been displayed
        SceneManager.LoadScene(this.currentLevel.sceneName, LoadSceneMode.Single);
    }

    public void StartCutscene()
    {

    }

    public Level GetActiveLevel()
    {
        var _activeScene = SceneManager.GetActiveScene();
        var _activeSceneName = _activeScene.name;
        //get active level from the scene name 
        Level _activeLevel = null;
        foreach (Level i in levelList)
        {
            if (i.sceneName == _activeSceneName)
            {
                _activeLevel = i;
                return _activeLevel;
            }
        }

        Debug.Log(_activeLevel);
        return _activeLevel;

    }

    public void ResetAllLevels()
    {
        foreach (Level i in levelList)
        {
            i.isActive = false;
            i.isCompleted = false;
        }
    }

    public void StartGame()
    {
        ResetAllLevels();
        GoToTheHub();
    }

    public void GoToFirstLevel()
    {
        SwitchLevel(this.currentLevel, this.musueumLevel);
    }

    public void GoToSecondLevel()
    {
        SwitchLevel(this.currentLevel, this.studioLevel);
    }

    public void GoToThirdLevel()
    {
        SwitchLevel(this.currentLevel, this.officeLevel);
    }

    public void GoToMainMenu()
    {
        SwitchLevel(this.currentLevel, this.mainMenuLevel);
    }

    public void GoToTheHub()
    {
        SwitchLevel(this.currentLevel, this.hubLevel);
    }

    public void GoToWinLoseLevel()
    {
        SwitchLevel(this.currentLevel, this.win_loseLevel);
    }

    public void GoToTutorialLevel()
    {
        SwitchLevel(this.currentLevel, this.win_loseLevel);
    }
    public void GoToMusuemLevel()
    {
        SwitchLevel(this.currentLevel, this.infoMusuem);
    }
    public void GoToStudioLevel()
    {
        SwitchLevel(this.currentLevel, this.infoStudio);
    }
    public void GoToOfficeLevel()
    {
        SwitchLevel(this.currentLevel, this.infoOffice);
    }
    public void GoToWinScreen()
    {
        SwitchLevel(this.currentLevel, this.win_loseLevel);
    }    
    public void QuitGame()
    {
        Application.Quit();
    }   

    public int tipCount;
    /*
    private IEnumerator DisplayLoadInformation(Level targetLevel)
    {
        tipCount = 0;
        this.tipText.text = targetLevel.objectiveList[tipCount];
        while (loadingScreen.activeInHierarchy)
        {
            yield return new WaitForSeconds(3f);

            loadCanvasGroup.alpha = Mathf.MoveTowards(0f, 0.5f, 0.25f);

            yield return new WaitForSeconds(3f);

            tipCount++;
            if (tipCount >= targetLevel.objectiveList.Count)
            {
                tipCount = 0;
            }

            this.tipText.text = targetLevel.objectiveList[tipCount];

            loadCanvasGroup.alpha = Mathf.MoveTowards(0f, 0.5f, 0.25f);
        }
    }*/

    private IEnumerator Wait()
    {
        _processingReference.FadeCircleOut();
        yield return new WaitForSeconds(2f);
        SwitchLevel(this.currentLevel, this.hubLevel);
        _processingReference.FadeCircleIn();
        //collided = false;
        yield break;
    }


}
