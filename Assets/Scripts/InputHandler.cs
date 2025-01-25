using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputHandler : Singleton<InputHandler>
{
    public UnityEvent<InputValue> OnMoveInput;
    public UnityEvent<InputValue> OnLookInput;
    public UnityEvent<InputValue> OnSprintInput;
    public UnityEvent<InputValue> OnFireInput;
    public UnityEvent<InputValue> OnScrollInput;
    public UnityEvent<InputValue> OnClearCanvasInput;
    public UnityEvent<InputValue> OnSubmitInput;

    public void OnMove(InputValue value)
    {
        OnMoveInput.Invoke(value);
    }

    public void OnLook(InputValue value)
    {
        OnLookInput.Invoke(value);
    }

    public void OnSprint(InputValue value)
    {
        OnSprintInput.Invoke(value);
    }

    public void OnFire(InputValue value)
    {
        OnFireInput.Invoke(value);
    }

    public void OnScroll(InputValue value)
    {
        OnScrollInput.Invoke(value);
    }

    public void OnClearCanvas(InputValue value)
    {
        OnClearCanvasInput.Invoke(value);
    }

    public void OnSubmit(InputValue value)
    {
        OnSubmitInput.Invoke(value);
    }
}
