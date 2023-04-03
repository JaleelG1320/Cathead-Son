using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLossManager : MonoBehaviour
{
    public static bool gameEnd = false;
    public static bool win = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameEnd == true){
            SceneManager.LoadScene(2);
            gameEnd = false;
        }
    }
}
