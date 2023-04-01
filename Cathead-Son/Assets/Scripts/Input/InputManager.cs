using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static ThirdPersonActionsAsset _inputActions = new ThirdPersonActionsAsset();
    public static event Action<InputActionMap> _actionMapChange; 



    // Start is called before the first frame update
    void Start()
    {
        ToggleActionMap(_inputActions.Player);
    }

    // Update is called once per frame
    public static void ToggleActionMap(InputActionMap actionMap)
    {
        if (actionMap.enabled)
        {
            _inputActions.Disable();
            _actionMapChange?.Invoke(actionMap);
            actionMap.Enable();
        }
    }
}
