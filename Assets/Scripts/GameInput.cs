using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    private InputActions inputActions;
    public event EventHandler OnMenuButtonPressed;
    private void Awake()
    {
        Instance = this;
        inputActions = new InputActions();
        inputActions.Enable();

        inputActions.Player.Menu.performed += Menu_performed;

    }

    private void Menu_performed(InputAction.CallbackContext context)
    {
        OnMenuButtonPressed?.Invoke(this, EventArgs.Empty);
    }

    public bool IsUpActionPressed()
    {
        return inputActions.Player.LanderUp.IsPressed();
    }

    public bool IsLeftActionPressed()
    {
        return inputActions.Player.LanderLeft.IsPressed();
    }
    public bool IsRightActionPressed()
    {
        return inputActions.Player.LanderRight.IsPressed();
    }

    private void OnDestroy()
    {
        inputActions.Disable();
    }
}
