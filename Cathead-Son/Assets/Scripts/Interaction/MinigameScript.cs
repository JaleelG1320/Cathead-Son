using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameScript : MonoBehaviour
{
    // Start is called before the first frame update
    private int[] gameArray;

    public Button[] buttonArray;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RandomNumArrayGen()
    {
        for (int i = 0; i <= gameArray.Length; i++)
        {
            gameArray[i] = Random.Range(1,4);
        }
    }

    public void ButtonCheck()
    {

    }

    private IEnumerator PlayMinigame()
    {
        RandomNumArrayGen();

        var i = 0;
        while (i < gameArray.Length)
        {


            
            yield return new WaitForSeconds(3f);
        }
        
    }
}
