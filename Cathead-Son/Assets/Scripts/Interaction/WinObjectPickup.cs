using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class WinObjectPickup : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "CurrentPlayer")
        {
            /*
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            WinLossManager.win = true;
            WinLossManager.gameEnd = true;
            */

            GameManager.instance.OnLevelComplete();
        }
    }
}
