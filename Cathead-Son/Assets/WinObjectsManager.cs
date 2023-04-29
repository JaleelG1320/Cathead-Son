using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinObjectsManager : MonoBehaviour
{
    public GameObject _winObject1;
    public GameObject _winObject2;
    public GameObject _winObject3;
    public GameObject _winObject4;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.instance.musueumLevel.isCompleted)
        {
            _winObject1.SetActive(true);
        }
        else
        {
            _winObject1.SetActive(false);
        }
        if (GameManager.instance.studioLevel.isCompleted)
        {
            _winObject2.SetActive(true);
        }
        else
        {
            _winObject2.SetActive(false);
        }
        if (GameManager.instance.officeLevel.isCompleted)
        {
            _winObject3.SetActive(true);
        }
        else
        {
            _winObject3.SetActive(false);
        }
        if (GameManager.instance.tutorialLevel.isCompleted)
        {
            _winObject4.SetActive(true);
        }
        else
        {
            _winObject4.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
