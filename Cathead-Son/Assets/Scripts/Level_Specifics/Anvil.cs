using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anvil : MonoBehaviour
{
    public LayerMask TargetMask;
    int count = 0;

    private void OnCollisionEnter(Collision other)
    {
        if (TargetMask == (TargetMask | (1 << other.gameObject.layer)))
        {
            count++;
            if (count > 1)
            {
                Destroy(gameObject);
                return;
            }
            BossEventTrigger.bossHitCount++;
            GetComponent<Rigidbody>().isKinematic = true;
            Debug.Log("Collision: " + gameObject.name);
            Destroy(gameObject);
            other.gameObject.GetComponentInChildren<Animator>().Play("Squish", 0, 0.0f);
        }
    }
}
