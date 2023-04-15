using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMover : MonoBehaviour
{
    public GameObject startMarker;
    public GameObject endMarker;

    public float speed = 1.0F;
    private float startTime;
    private float journeyLength;
    
    bool Tunnel = false;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker.transform.position, endMarker.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(!Tunnel){
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(startMarker.transform.position, endMarker.transform.position, Mathf.PingPong(fracJourney, 1));
            if(fracJourney > .95){
                Tunnel = true;
                Debug.Log("Tunnel True");
            }
        }
        if(Tunnel){
            gameObject.SetActive(false);
            Invoke(nameof(RespawnCar), 3);
            Tunnel = false;
        }

    }
    void RespawnCar(){
        gameObject.transform.position = startMarker.transform.position;
        gameObject.SetActive(true);
        startTime = Time.time;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CurrentPlayer"))
        {
            GameManager.instance.OnLevelFailed();
        }
    }
}
