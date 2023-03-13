using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    public void SwapCharacterPrev()
    {
        whichCharacter = (whichCharacter - 1 + possibleCharacters.Count) % possibleCharacters.Count;
        Swap();
    }

    public void SwapCharacterNext()
    {
        whichCharacter = (whichCharacter - 1 + possibleCharacters.Count) % possibleCharacters.Count;
        Swap();
    }

    public void Swap()
    {
        character = possibleCharacters[whichCharacter];
        character.GetComponent<ThirdPersonController>().enabled = true;
        for(int  i = 0; i < possibleCharacters.Count; i++)
        {
            if(possibleCharacters[i] != character)
            {
                possibleCharacters[i].GetComponent<ThirdPersonController>().enabled = false;
            }
        }

    }
}
