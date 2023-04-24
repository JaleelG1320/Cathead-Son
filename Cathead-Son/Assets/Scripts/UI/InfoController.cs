using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoController : MonoBehaviour
{
    public Slider loadingBar;
    public GameObject slider;
    public GameObject startText;
    public Button button;

    public bool gameStart = false;
    // Start is called before the first frame update
    void Start()
    {
        loadingBar.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        loadingBar.value += 1;
        Invoke(nameof(startLevel), 5f);
        if(gameStart){
            button.Select();
            
        }
    }
    void startLevel(){
        slider.SetActive(false);
        gameStart = true;
        startText.SetActive(true);
    }
}
