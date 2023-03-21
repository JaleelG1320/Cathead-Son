using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractableScript : InteractableObjects
{
    public override void OnInteract()
    {
        Debug.Log("Interacted with " + gameObject.name);
    }
    public override void OnFocus()
    {
        Debug.Log("Focused on " + gameObject.name);
    }
    public override void OnLoseFocus()
    {
        Debug.Log("Lost Focus on " + gameObject.name);
    }
}
