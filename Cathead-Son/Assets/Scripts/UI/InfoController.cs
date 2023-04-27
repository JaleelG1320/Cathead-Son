using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoController : MonoBehaviour
{
    public Button button;
    public MaskableGraphic imageToToggle;
 
     public float interval = 1f;
     public float startDelay = 0.5f;
     public bool currentState = true;
     public bool defaultState = true;
     bool isBlinking = false;

    public bool gameStart = false;
    // Start is called before the first frame update
    void Start()
    {
        imageToToggle.enabled = defaultState;
        imageToToggle.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Invoke(nameof(startLevel), 5f);
        if(gameStart){
            button.Select();
            StartBlink();
        }
    }
    void startLevel(){
        gameStart = true;
    }
    public void StartBlink()
     {
         // do not invoke the blink twice - needed if you need to start the blink from an external object
         if (isBlinking)
             return;
 
         if (imageToToggle !=null)
         {
             isBlinking = true;
             InvokeRepeating("ToggleState", startDelay, interval);
         }
     }
    public void ToggleState()
     {
         imageToToggle.enabled = !imageToToggle.enabled;
     }
}
