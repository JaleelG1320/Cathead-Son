using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager :  MonoBehaviour
{
    [Header("Level References")]
    Level hubLevel = new Level(0, true, false, "Start a level");
    Level musueumLevel = new Level(1, false, false, "Steal the Ring");
    Level studioLevel = new Level(2, false, false, "Steal the Emmy");
    Level officeLevel = new Level(3, false, false, "Get your revenge on Richard Rat");

    [Header("Game Settings")]
    public int volumeSettings;
    public bool vSync;
    public bool bnwActive;

    [Header("Objective Lists")]
    private List<string> hubObjectives = new List<string>();
    private List<string> musueumObjectives = new List<string>();
    private List<string> studioObjectives = new List<string>();
    private List<string> officeObjectives = new List<string>();

    [Header("Script References")]
    [SerializeField] private PostProcessingManager _processingReference;
    [SerializeField] private CharacterSwap _swapReference;
    [SerializeField] private PauseMenu _pauseReference;

    public static GameManager instance; //reference to game manager script
    

    void Awake()
    {
        if (instance != null)
        {
            instance = this;
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
        
    }

    //controls what happens when the player wins a level
    public void OnLevelComplete()
    {
        // play wipe effect

        // set current level to be completed

        //set current level to not be active

        // take player back to hub level

        // 
    }

    public void OnLevelFailed()
    {

    }

    


}
