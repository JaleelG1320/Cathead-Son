using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwap : MonoBehaviour
{
    public Transform character;
    public List<Transform> possibleCharacters;
    public int whichCharacter;

    private void Start()
    {
        if(character == null && possibleCharacters.Count == 1)
        {
            character = possibleCharacters[0];
        }
        Swap();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            whichCharacter = (whichCharacter - 1 + possibleCharacters.Count) % possibleCharacters.Count;
            Swap();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            whichCharacter = (whichCharacter + 1) % possibleCharacters.Count;
            Swap();
        }
    }

    public void Swap()
    {
        character = possibleCharacters[whichCharacter];
        character.GetComponent<PlayerController>().enabled = true;
        for(int  i = 0; i < possibleCharacters.Count; i++)
        {
            if(possibleCharacters[i] != character)
            {
                possibleCharacters[i].GetComponent<PlayerController>().enabled = false;
            }
        }

    }
}
