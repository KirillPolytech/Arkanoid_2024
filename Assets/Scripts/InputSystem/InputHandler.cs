using System;
using UnityEngine;
using Zenject;

namespace Arkanoid.InputSystem
{
    public class InputHandler : ITickable, IFixedTickable
    {
        public Action<InputData> OnInputDataUpdate;
        public Action<InputData> OnInputDataUpdateFixed;

        private InputData _inputData;

        public void Tick()
        {
            _inputData.HorizontalInputValue = Input.GetAxis(GlobalVariables.HorizontalInput);
            _inputData.EscapePressed = Input.GetKeyDown(GlobalVariables.Escape);
            _inputData.IsLMBPressed = Input.GetMouseButtonDown(GlobalVariables.LMB);

            OnInputDataUpdate?.Invoke(_inputData);
        }

        public void FixedTick()
        {
            OnInputDataUpdateFixed?.Invoke(_inputData);
        }
    }
}