using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLossScreen : MonoBehaviour
{
    public GameObject WinUI;
    public GameObject LoseUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(WinLossManager.win == false){
            WinUI.SetActive(false);
            LoseUI.SetActive(true);
        }
        else if (WinLossManager.win == true){
            WinUI.SetActive(true);
            LoseUI.SetActive(false);
        }
    }
}
