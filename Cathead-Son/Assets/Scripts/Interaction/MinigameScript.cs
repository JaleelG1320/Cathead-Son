using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int[] _pickedButtons;
    public Button[] Buttons;

    public int _currentNumInSequence = 0;
    public void Awake()
    {
        Buttons[0] = GameObject.Find("YellowButton").GetComponent<Button>();
        Buttons[1] = GameObject.Find("RedButton").GetComponent<Button>();
        Buttons[2] = GameObject.Find("GreenButton").GetComponent<Button>();
        Buttons[3] = GameObject.Find("BlueButton").GetComponent<Button>();

        Buttons[0].onClick.AddListener(() => ButtonCheck(Buttons[0]));
        Buttons[1].onClick.AddListener(() => ButtonCheck(Buttons[1]));
        Buttons[2].onClick.AddListener(() => ButtonCheck(Buttons[2]));
        Buttons[3].onClick.AddListener(() => ButtonCheck(Buttons[3]));


        DisableButtons();
    }
    void OnEnable()
    {
        _pickedButtons = new int[Buttons.Length];
        RandomNumArrayGen();
        StartCoroutine(PlayMinigame());
    }
    private void OnDisable()
    {
        Debug.Log("Disabling");
        GetComponent<HackingInteractableScript>().TriggerEvent();
        GetComponent<HackingInteractableScript>().SwitchToPlayer();
        DisableButtons();
    }
    private void RandomNumArrayGen()
    {
        System.Random a = new System.Random();
        List<int> chosenNumbers = new List<int>(Buttons.Length);
        for (int i = 0; i < Buttons.Length; i++)
        {
            int chosenNum;
            do
                 chosenNum = a.Next(0, 4);
            while (chosenNumbers.Contains(chosenNum)); // Avoids picking the same button twice.

            chosenNumbers.Add(chosenNum);
            _pickedButtons[i] = chosenNum;
        }
    }

    public void ButtonCheck(Button clickedButton)
    {
        Button correctButton = Buttons[_pickedButtons[_currentNumInSequence]];
        if (clickedButton == correctButton) // Player clicked the right button.
        {
            Debug.Log("Player selected Correct button!");
            _currentNumInSequence++; // Move on to the next button.
        }
        else // Player clicked the wrong button.
        {
            Debug.Log("Player selected WRONG button!");
            InputManager.ToggleActionMap(InputManager._inputActions.Player);
            StartCoroutine(PlayMinigame()); // Reset the minigame.
        }


        if (_currentNumInSequence >= _pickedButtons.Length) // Player won!
            OpenDoor();
    }
    public void OpenDoor()
    {
        Debug.Log("Opening door!");

        this.enabled = false;
    }

    private IEnumerator PlayMinigame()
    {
        RandomNumArrayGen();
        _currentNumInSequence = 0;
        DisableButtons();
        var i = 0;
        while (i < _pickedButtons.Length)
        {
            Button currentButton = Buttons[_pickedButtons[i]];

            Color color = currentButton.image.color;
            currentButton.image.color = currentButton.colors.selectedColor;

            yield return new WaitForSeconds(1f);

            i++;
            currentButton.image.color = color;
            ResetButtons();
        }
        InputManager.ToggleActionMap(InputManager._inputActions.Player);
        Buttons[Buttons.Length - 1].Select();
    }

    private void DisableButtons()
    {
        foreach (Button button in Buttons)
        {
            button.interactable = false;
        }
    }

    private void ResetButtons()
    {
        foreach (Button button in Buttons)
        {
            button.interactable = true;
        }
    }
}
