using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateManager : MonoBehaviour
{
    public GameObject[] doorTrigger;
    public static bool plate1;
    public static bool plate2;
    public AudioClip trigger;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        plate1 = false;
        plate2 = false;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (plate1 && plate2)
        {
            foreach (GameObject go in doorTrigger)
            {
                if (doorTrigger is not null && go.TryGetComponent(out ITriggerable triggerable))
                {
                    triggerable.OnTrigger();
                    audioSource.PlayOneShot(trigger, 0.7F);
                }
            }
            
        }
    }
}
