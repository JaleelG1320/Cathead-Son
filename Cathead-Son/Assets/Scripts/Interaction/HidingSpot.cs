using System.Collections.Generic;
using UnityEngine;

public class HidingSpot: InteractableObjects
{
    public bool IsHidingHere;
    public static ThirdPersonController PlayerController;
    public Camera HidingCam;
    private Vector3 _lastPos;
    public static List<HidingSpot> HidingSpots = new List<HidingSpot>();

    public void Start()
    {
        HidingSpots.Add(this);
    }

    public static void UpdatePlayer(Transform character)
    {
        PlayerController = character.GetComponent<ThirdPersonController>();
    }

    public override void OnInteract() {

        IsHidingHere = !IsHidingHere;

        if(IsHidingHere)
        {
            // Hide Player & Disable Releveant Functionality
            PlayerController.GetComponent<CapsuleCollider>().enabled = false;
            PlayerController.GetComponent<Rigidbody>().isKinematic = true;
            PlayerController.GetComponent<ThirdPersonController>().IsHiding = true;
            HidingCam.enabled = true;

            //player.ToggleOutLineOfClosestHidingSpot(false);
            
            // Store Last Position & Move Player to Hiding Spot Position
            _lastPos = PlayerController.gameObject.transform.position;
            Debug.Log(PlayerController.gameObject.transform.position);
            PlayerController.gameObject.transform.position = transform.position;
            //outline.enabled = false;
        }
        else if (!IsHidingHere) // Inverse
        {
            PlayerController.gameObject.transform.position = _lastPos;
            Debug.Log(PlayerController.gameObject.transform.position);
            PlayerController.GetComponent<ThirdPersonController>().IsHiding = false;
            PlayerController.GetComponentInChildren<CapsuleCollider>().enabled = true;
            PlayerController.GetComponent<Rigidbody>().isKinematic = false;
            //outline.enabled = true;
        }
    }

    public override void OnFocus()
    {
        //outline.enabled = true;
    }

    public override void OnLoseFocus()
    {
        //outline.enabled = false;
    }
}
