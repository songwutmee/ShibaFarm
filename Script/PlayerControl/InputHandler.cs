using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance { get; private set; }

    public Vector2 MoveInput { get; private set; }
    public bool InteractTriggered { get; private set; }
    public bool ActionTriggered { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void OnMove(InputValue value)
    {
        MoveInput = value.Get<Vector2>();
    }

    public void OnInteract(InputValue value)
    {
        InteractTriggered = value.isPressed;
    }

    public void OnAction(InputValue value)
    {
        ActionTriggered = value.isPressed;
    }

    public void LateUpdate()
    {
        InteractTriggered = false;
        ActionTriggered = false;
    }
}