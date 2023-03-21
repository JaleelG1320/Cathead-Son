using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HidingCamera : MonoBehaviour
{
    float inputX;
    private void Start()
    {
        enabled = false;
    }
    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        if(inputX != 0)
        {
            rotate();
        }
    }

    private void rotate()
    {
        transform.Rotate(new Vector3(0f, inputX * Time.deltaTime * 40, 0f));
    }
}
