using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Events;

using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Input Reader")]
public class InputAssetController : ScriptableObject, ThirdPersonActionsAsset.IPlayerActions, ThirdPersonActionsAsset.IUIActions
{

	[Header("Events")]
	private ThirdPersonActionsAsset _inputActions;
	public event Action<Vector2> MoveEvent;
	public event Action<Vector2> LookEvent;
	public event Action SprintEvent;
	public event Action SprintCanceledEvent;
	public event Action JumpEvent;
	public event Action CrouchEvent;
	public event Action InteractEvent;
	public event Action SwapEvent;
	public event Action ResumeEvent;
	public event Action PauseEvent;

	private void OnEnable()
	{
		if (_inputActions == null)
		{
			_inputActions = new ThirdPersonActionsAsset();
		}

		_inputActions.Player.SetCallbacks(this);
		_inputActions.UI.SetCallbacks(this);
	}
	
	private void SetUI()
	{
		_inputActions.Player.Disable();
		_inputActions.UI.Enable();
	}

	private void SetPlayer()
	{
		_inputActions.Player.Enable();
		_inputActions.UI.Disable();
	}


	public void OnMove(InputAction.CallbackContext input)
	{
		MoveEvent?.Invoke(input.ReadValue<Vector2>());
	}

	public void OnSprint(InputAction.CallbackContext input)
	{
		if (input.phase == InputActionPhase.Performed)
		{
			SprintEvent?.Invoke();
		}
		if (input.phase == InputActionPhase.Canceled)
		{
			SprintCanceledEvent?.Invoke();
		}
	}

	public void OnLook(InputAction.CallbackContext input)
	{
		LookEvent?.Invoke(input.ReadValue<Vector2>());
	}

	public void OnJump(InputAction.CallbackContext input)
	{
		JumpEvent?.Invoke();
	}

	public void OnCrouch(InputAction.CallbackContext input)
	{
		CrouchEvent?.Invoke();
	}

	public void OnInteract(InputAction.CallbackContext input)
	{
		InteractEvent?.Invoke();
	}

	public void OnSwap(InputAction.CallbackContext input)
	{
		SwapEvent?.Invoke();
	}

	public void OnPause(InputAction.CallbackContext input)
	{
		if (input.phase == InputActionPhase.Performed)
		{
			PauseEvent?.Invoke();
			SetUI();
		}
		
	}

	public void OnResume(InputAction.CallbackContext input)
	{
		if (input.phase == InputActionPhase.Performed)
		{
			ResumeEvent?.Invoke();
			SetPlayer();
		}
	}

	public void OnNavigate(InputAction.CallbackContext input)
	{

	}

		public void OnSubmit(InputAction.CallbackContext input)
	{

	}

		public void OnCancel(InputAction.CallbackContext input)
	{

	}

		public void OnPoint(InputAction.CallbackContext input)
	{

	}

		public void OnClick(InputAction.CallbackContext input)
	{

	}

		public void OnScrollWheel(InputAction.CallbackContext input)
	{

	}

		public void OnMiddleClick(InputAction.CallbackContext input)
	{

	}

		public void OnRightClick(InputAction.CallbackContext input)
	{

	}

}

