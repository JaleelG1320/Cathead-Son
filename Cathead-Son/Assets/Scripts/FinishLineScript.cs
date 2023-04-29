using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineScript : MonoBehaviour
{
    private bool collided;
    public PostProcessingManager _postProcessReference;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var collision = new Collider();
        if (!collided)
        {
            StopCoroutine(Wait(collision));
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "CurrentPlayer")
        {
            collided = true;
            StartCoroutine(Wait(collision));
        }
    }

    private IEnumerator Wait(Collider collision)
    {
        _postProcessReference.FadeCircleOut();
        yield return new WaitForSeconds(2f);
        collision.gameObject.transform.position = new Vector3(0, 1, 0);
        _postProcessReference.FadeCircleIn();
        collided = false;
        yield break;
    }
}
