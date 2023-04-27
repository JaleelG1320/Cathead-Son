using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchInteract : InteractableObjects
{
    public Animator anim;

    void Awake(){
        //anim = GetComponent<Animator>();
        anim.SetBool("Change", false);
    }
    public override void OnInteract()
    {
        Debug.Log("Interacted with " + gameObject.name);
        this.TriggerEvent();
        anim.enabled = true;
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
