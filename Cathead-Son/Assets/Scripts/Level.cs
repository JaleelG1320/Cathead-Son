using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public bool isCompleted; //true is completed, false is not completed
    public bool isActive; //if current level is the one that is playing
    public int levelNum; //0 is hub, 1 is museusm, 2 is studio, 3 is office
    public string levelObjective; //to tell the player what to do 
    public List<string> objectiveList; //used to get all objectives for a level
    public int currentObjNum = 0; //number to track which object player is at

    public Level(int _levelNum, bool _isActive, bool _isCompleted, string _levelObjective)
    {
        this.levelNum = _levelNum;
        this.isActive = _isActive;
        this.isCompleted = _isCompleted;
        this.levelObjective = _levelObjective;
    }

    public void NextObjective()
    {
        this.objectiveList.RemoveAt(currentObjNum);
        this.currentObjNum++;
    }

}
