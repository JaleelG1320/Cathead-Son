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
    private GameObject chON;
    private GameObject chOFF;
    private GameObject tON;
    private GameObject tOFF;

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
        //chON = GameObject.Find("CatheadOn");
        //chOFF = GameObject.Find("CatheadOff");
        //tON = GameObject.Find("TommyOn");
        //tOFF = GameObject.Find("TommyOff");
        //chOFF.SetActive(false);
        //tON.SetActive(false);
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
                if(PossibleCharacters[i].gameObject.name == "Cat Head")
                {
                    chOFF.SetActive(true);
                    tON.SetActive(true);
                    chON.SetActive(false);
                    tOFF.SetActive(false);
                }
                if(PossibleCharacters[i].gameObject.name == "Son")
                {
                    chOFF.SetActive(false);
                    tON.SetActive(false);
                    chON.SetActive(true);
                    tOFF.SetActive(true);
                }
            }
        }

    }
}
