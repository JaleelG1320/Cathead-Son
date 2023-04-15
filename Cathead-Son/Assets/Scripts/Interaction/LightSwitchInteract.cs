using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchInteract : InteractableObjects
{
    public GameObject light;
    public Animator anim;

    void Awake(){
        //anim = GetComponent<Animator>();
        anim.SetBool("Change", false);
    }
    public override void OnInteract()
    {
        Debug.Log("Interacted with " + gameObject.name);
        anim.Play("Flip", -1, 0f);
        this.TriggerEvent();
        //Destroy(light);
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
