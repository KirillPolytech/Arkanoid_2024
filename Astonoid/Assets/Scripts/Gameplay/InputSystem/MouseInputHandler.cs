using System;
using UnityEngine;
using Zenject;

public class MouseInputHandler : IInputHandler, ITickable, IFixedTickable
{
    private readonly MouseSensivity _mouseSensivity;
    
    public Action<InputData> OnInputDataUpdate { get; set; }
    public Action<InputData> OnInputDataUpdateFixed { get; set; }
    
    private InputData _inputData;

    public MouseInputHandler(MouseSensivity mouseSensivity)
    {
        _mouseSensivity = mouseSensivity;
    }

    public void HandleInput()
    {
        _inputData.HorizontalInputValue = Input.mousePositionDelta.x * _mouseSensivity.MouseSensivityValue;
        _inputData.EscapePressed = Input.GetKeyDown(GlobalVariables.Escape);
        _inputData.IsStartGameButtonPressed = Input.GetMouseButtonDown(GlobalVariables.LMB);
    }
    
    public void Tick()
    {
        HandleInput();

        OnInputDataUpdate?.Invoke(_inputData);
    }

    public void FixedTick()
    {
        OnInputDataUpdateFixed?.Invoke(_inputData);
    }
}