using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Switch : InteractableObjects
{
    public Animator anim;
    public override void OnInteract()
    {
        anim.Play("Flip", -1, 0f);
        TriggerEvent();
    }
    public override void OnFocus()
    {
    }
    public override void OnLoseFocus()
    {
    }


}
