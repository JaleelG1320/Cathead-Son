using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
// Transforms to act as start and end markers for the journey.
public Transform startMarker;
public Transform endMarker;

// Movement speed in units/sec.
public float speed = 1.0F;
bool swap = false;

// Time when the movement started.
private float startTime;

// Total distance between the markers.
private float journeyLength;

void Start()
{
// Keep a note of the time the movement started.
startTime = Time.time;

// Calculate the journey length.
journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
}

// Follows the target position like with a spring
void Update()
{
    float horizontalInput = startMarker.position.z;
    float verticalInput = startMarker.position.z;
    // Distance moved = time * speed.
    float distCovered = (Time.time - startTime) * speed;
    

    // Fraction of journey completed = current distance divided by total distance.
    float fracJourney = distCovered / journeyLength;

    Vector3 movementDirection = new Vector3(horizontalInput, -1, verticalInput);
    movementDirection.Normalize();

    // Set our position as a fraction of the distance between the markers and pingpong the movement.
    transform.position = Vector3.Lerp(startMarker.position, endMarker.position, Mathf.PingPong(fracJourney, 1));
}
private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "CurrentPlayer")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            WinLossManager.gameEnd = false;
            WinLossManager.gameEnd = true;
            
        }
    }
}
