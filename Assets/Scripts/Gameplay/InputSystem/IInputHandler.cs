using System;

public interface IInputHandler
{
    public Action<InputData> OnInputDataUpdate { get; set; }
    public Action<InputData> OnInputDataUpdateFixed { get; set; }
    
    public void HandleInput(){}
}
