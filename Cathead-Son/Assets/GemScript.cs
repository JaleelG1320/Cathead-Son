using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemScript : InteractableObjects
{
    public override void OnFocus()
    {
        
    }

    public override void OnInteract()
    {
        GameManager.instance.OnLevelComplete();
    }

    public override void OnLoseFocus()
    {
        
    }
}
