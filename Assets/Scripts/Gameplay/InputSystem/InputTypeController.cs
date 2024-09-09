using System;

namespace Arkanoid.InputSystem
{
    public class InputTypeController
    {
        public IInputHandler CurrentInputHandler { get; private set; }

        public InputTypeController(UserData userData, 
            KeyboardInputHandler keyboardInputHandler, 
            MouseInputHandler mouseInputHandler)
        {
            CurrentInputHandler = userData.ControlType switch
            {
                ControlType.keyboard => keyboardInputHandler,
                ControlType.mouse => mouseInputHandler,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}