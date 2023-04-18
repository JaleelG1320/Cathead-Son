using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour , ITriggerable
{
    public Transform EndTF;
    public float LiftSpeed;

    private bool openDoor;
    public void OnTrigger()
    {
        EndTF.SetParent(null);
        openDoor = true; 
    }

    public void Update()
    {
        if(openDoor && transform.position != EndTF.transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, EndTF.position, LiftSpeed * Time.deltaTime);
        }
    }
}
