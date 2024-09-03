using System;
using UnityEngine;
using Zenject;

namespace Arkanoid.InputSystem
{
    public class InputHandler : ITickable
    {
        public Action<InputData> OnInputDataUpdate;

        private InputData _inputData;

        public void Tick()
        {
            _inputData.HorizontalInputValue = Input.GetAxis(GlobalVariables.HorizontalInput);
            _inputData.EscapePressed = Input.GetKeyDown(GlobalVariables.Escape);

            OnInputDataUpdate?.Invoke(_inputData);
        }
    }
}