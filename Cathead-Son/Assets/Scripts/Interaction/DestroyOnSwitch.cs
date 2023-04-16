using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnSwitch : MonoBehaviour, ITriggerable
{
    public void OnTrigger()
    {
        Destroy(this.gameObject);
    }
}
