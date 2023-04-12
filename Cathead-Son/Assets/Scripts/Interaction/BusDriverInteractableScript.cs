using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusDriverInteractableScript : InteractableObjects
{
    public GameObject interactPrefab;
    private GameObject interactIcon;
    private CharacterSwap swapReference;
    public override void OnFocus()
    {
        swapReference = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<CharacterSwap>();
        interactIcon = Instantiate(interactPrefab, gameObject.transform.position, Quaternion.identity);
        
    }

    public override void OnInteract()
    {
        //
        
    }

    public override void OnLoseFocus()
    {
        Destroy(interactIcon);
        
    }
}
