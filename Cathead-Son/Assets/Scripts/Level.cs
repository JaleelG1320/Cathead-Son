using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level")]
public class Level : ScriptableObject
{
    public bool isCompleted; //true is completed, false is not completed
    public bool isActive; //if current level is the one that is playing
    public int levelNum; //0 is hub, 1 is museusm, 2 is studio, 3 is office
    public string levelObjective; //to tell the player what to do 
    public List<string> objectiveList; //used to get all objectives for a level
    public int currentObjNum; //number to track which object player is at
    public string sceneName; //name of the scene for the level

}
