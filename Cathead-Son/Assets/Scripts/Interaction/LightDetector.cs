using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDetector : MonoBehaviour
{
private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CurrentPlayer"))
        {
            Debug.Log("LOSE");
        }
    }
}
