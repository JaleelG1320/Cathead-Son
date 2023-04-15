using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVInteractableScript : InteractableObjects
{

    private bool inHub;
    public override void OnFocus()
    {
        Debug.Log("Focused on " + gameObject.name);
        if (GameManager.instance.currentLevel.sceneName == "HubLevel")
        {
            inHub = true;
        }
        else
        {
            inHub = false;
        }
    }

    public override void OnInteract()
    {
        Debug.Log("Interacted");

        if (inHub)
        {
            GameManager.instance.SwitchLevel(GameManager.instance.currentLevel, GameManager.instance.tutorialLevel);
        }
        else
        {
            GameManager.instance.SwitchLevel(GameManager.instance.currentLevel, GameManager.instance.hubLevel);
        }
        
    }

    public override void OnLoseFocus()
    {
        Debug.Log("Lost focus on " + gameObject.name);
    }
}
