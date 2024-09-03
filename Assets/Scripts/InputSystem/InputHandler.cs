using System;
using Zenject;

public class InputHandler : ITickable
{
    public Action<InputData> OnInputDataUpdate;
    
    private InputData _inputData;
    
    public void Tick()
    {
        OnInputDataUpdate?.Invoke(_inputData);
    }
}
