using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [Header("Game Settings")]
    public int volumeSettings;
    public bool vSync;
    public bool bnwActive;
    public Level currentLevel;

    [Header("Objective Lists")]
    private List<string> hubObjectives = new List<string>();
    private List<string> musueumObjectives = new List<string>();
    private List<string> studioObjectives = new List<string>();
    private List<string> officeObjectives = new List<string>();

    [Header("Script References")]
    [SerializeField] private PostProcessingManager _processingReference;
    [SerializeField] private CharacterSwap _swapReference;
    [SerializeField] private PauseMenu _pauseReference;

    [Header("Level List")]
    public List<Level> levelList = new List<Level>();

    public static GameManager instance; //reference to game manager script
    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            levelList.Add(hubLevel);
            levelList.Add(musueumLevel);
            levelList.Add(studioLevel);
            levelList.Add(officeLevel);
            levelList.Add(tutorialLevel);
            levelList.Add(win_loseLevel);
            levelList.Add(mainMenuLevel);
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
        // take player back to hub level
        SwitchLevel(this.currentLevel, this.hubLevel);
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
        this.currentLevel.isActive = false;
        //change current level to target level
        this.currentLevel = _targetLevel;
        //set target level to active 
        this.currentLevel.isActive = true;
        //change scene
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


}
