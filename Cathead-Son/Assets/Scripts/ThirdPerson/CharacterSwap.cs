using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CharacterSwap : MonoBehaviour
{
    public Transform Character;
    public List<Transform> PossibleCharacters;
    private int _whichCharacter;
    public CinemachineFreeLook CVirCam;

    [HideInInspector] public GameObject currentPlayer;

    private void Start()
    {
        CameraSwitcher.Register(CVirCam.gameObject.GetComponent<CinemachineVirtualCameraBase>());

        if (Character == null && PossibleCharacters.Count == 1)
        {
            Character = PossibleCharacters[0];
            Character.gameObject.tag = "CurrentPlayer";
            currentPlayer = Character.gameObject;
        }
        Swap();
    }

    public void SwapCharacterPrev()
    {
        _whichCharacter = (_whichCharacter - 1 + PossibleCharacters.Count) % PossibleCharacters.Count; // Update our player index.
        Swap();
    }

    public void Swap()
    {

        Character = PossibleCharacters[_whichCharacter];
        Character.GetComponent<ThirdPersonController>().enabled = true;

        HidingSpot.UpdatePlayer(Character);

        CVirCam.Follow = Character;
        CVirCam.LookAt = Character;
        Character.gameObject.tag = "CurrentPlayer";
        currentPlayer = Character.gameObject;

        for (int  i = 0; i < PossibleCharacters.Count; i++)
        {
            if(PossibleCharacters[i] != Character)
            {
                PossibleCharacters[i].GetComponent<ThirdPersonController>().enabled = false;
                PossibleCharacters[i].gameObject.tag = "IdlePlayer";
            }
        }

    }
}
