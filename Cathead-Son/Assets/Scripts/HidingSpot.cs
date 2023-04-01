using System.Collections.Generic;
using UnityEngine;

public class HidingSpot: Interactable
{
    public bool isHidingHere;
    public static ThirdPersonController player;
    public Camera hidingView;
    public Vector3 lastPos;
    public static List<HidingSpot> hidingSpots = new List<HidingSpot>();

    //public Outline outline;

    public void Start()
    {
        hidingSpots.Add(this);
    }

    public static void UpdatePlayer(Transform character)
    {
        player = character.GetComponent<ThirdPersonController>();
    }

    public override void Interact() {

        isHidingHere = !isHidingHere;

        if(isHidingHere)
        {
            // Hide Player & Disable Releveant Functionality
            player.GetComponent<CapsuleCollider>().enabled = false;
            player.GetComponent<Rigidbody>().isKinematic = true;
            hidingView.enabled = true;

            //player.ToggleOutLineOfClosestHidingSpot(false);
            
            // Store Last Position & Move Player to Hiding Spot Position
            lastPos = player.gameObject.transform.position;
            Debug.Log(player.gameObject.transform.position);
            player.gameObject.transform.position = transform.position;
            //outline.enabled = false;
        }
        else if (!isHidingHere) // Inverse
        {
            player.gameObject.transform.position = lastPos;
            Debug.Log(player.gameObject.transform.position);

            player.GetComponentInChildren<CapsuleCollider>().enabled = true;
            player.GetComponent<Rigidbody>().isKinematic = false;
            //outline.enabled = true;
        }
    }
}
