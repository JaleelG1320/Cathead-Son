using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateManager : MonoBehaviour
{
    public GameObject[] doorTrigger;
    public static int plateAmount;
    // Start is called before the first frame update
    void Start()
    {
        plateAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (plateAmount >= 2)
        {
            foreach (GameObject go in doorTrigger)
            {
                if (doorTrigger is not null && go.TryGetComponent(out ITriggerable triggerable))
                {
                    triggerable.OnTrigger();
                }
            }
            
        }
    }
}
