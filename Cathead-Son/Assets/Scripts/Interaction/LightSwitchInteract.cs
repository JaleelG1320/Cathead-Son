using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchInteract : InteractableObjects
{
    public GameObject light;
    public override void OnInteract()
    {
        Debug.Log("Interacted with " + gameObject.name);
        Destroy(light);
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
