using System;
using UnityEngine;
using Zenject;

namespace Arkanoid.InputSystem
{
    public class KeyboardInputHandler : IInputHandler, ITickable, IFixedTickable
    {
        public Action<InputData> OnInputDataUpdate { get; set; }
        public Action<InputData> OnInputDataUpdateFixed { get; set; }

        private InputData _inputData;

        public void HandleInput()
        {
            _inputData.HorizontalInputValue = Input.GetAxisRaw(GlobalVariables.HorizontalInput);
            _inputData.EscapePressed = Input.GetKeyDown(GlobalVariables.Escape);
            _inputData.IsStartGameButtonPressed = Input.GetKeyDown(GlobalVariables.Space);
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
}