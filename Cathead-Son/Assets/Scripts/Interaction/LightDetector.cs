using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class LightDetector : MonoBehaviour , ITriggerable
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CurrentPlayer"))
        {
            GameManager.instance.OnLevelFailed();
        }
    }
    public void OnTrigger()
    {
        Destroy(gameObject);
    }
}

