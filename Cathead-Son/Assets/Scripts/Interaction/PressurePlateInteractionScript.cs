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
        PressurePlateManager.plateAmount++;
        Debug.Log(PressurePlateManager.plateAmount);
    }

    void OnTriggerExit(Collider collision)
    {
        PressurePlateManager.plateAmount--;
        Debug.Log(PressurePlateManager.plateAmount);
    }
}
