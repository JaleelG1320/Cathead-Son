using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObjects : MonoBehaviour
{
    [Header("Trigger Object")]
    public GameObject triggerObject;
    public abstract void OnFocus();
    public abstract void OnInteract();
    public abstract void OnLoseFocus();

    public void TriggerEvent()
    {
        if (triggerObject is not null &&
            triggerObject.TryGetComponent(out ITriggerable triggerable))
        {
            triggerable.OnTrigger();
        }
    }
}
