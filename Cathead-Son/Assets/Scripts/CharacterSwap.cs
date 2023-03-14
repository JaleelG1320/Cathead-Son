using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CharacterSwap : MonoBehaviour
{
    public Transform character;
    public List<Transform> possibleCharacters;
    public int whichCharacter;
    public CinemachineVirtualCamera cVirCam;

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
        Debug.Log("test swap prev");
        whichCharacter = (whichCharacter - 1 + possibleCharacters.Count) % possibleCharacters.Count;
        Swap();
    }

    public void SwapCharacterNext()
    {
        Debug.Log("test swap next");
        whichCharacter = (whichCharacter - 1 + possibleCharacters.Count) % possibleCharacters.Count;
        Swap();
    }

    public void Swap()
    {
        character = possibleCharacters[whichCharacter];
        character.GetComponent<ThirdPersonController>().enabled = true;
        cVirCam.Follow = character;
        for(int  i = 0; i < possibleCharacters.Count; i++)
        {
            if(possibleCharacters[i] != character)
            {
                possibleCharacters[i].GetComponent<ThirdPersonController>().enabled = false;
            }
        }

    }
}
