using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateInteractionScript : MonoBehaviour
{
    public AudioClip trigger;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
            audioSource.PlayOneShot(trigger, 0.7F);
        }

        if (gameObject.name == "plate_2")
        {
            PressurePlateManager.plate2 = true;
            audioSource.PlayOneShot(trigger, 0.7F);
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
