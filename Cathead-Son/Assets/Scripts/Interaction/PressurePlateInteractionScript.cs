using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateInteractionScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (gameObject.name == "plate_1")
        {
            PressurePlateManager.plate1 = true;
        }

        if (gameObject.name == "plate_2")
        {
            PressurePlateManager.plate2 = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (gameObject.name == "plate_1")
        {
            PressurePlateManager.plate1 = false;
        }

        if (gameObject.name == "plate_2")
        {
            PressurePlateManager.plate2 = false;
        }
    }
}
