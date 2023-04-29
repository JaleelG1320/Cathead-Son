using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class WinObjectPickup : InteractableObjects
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
