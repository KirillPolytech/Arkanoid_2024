using System;
using Zenject;

public class PlatformPresenter : IDisposable
{
    public Action OnLose;
    
    private readonly PlatformModel _platformModel;
    private readonly PlatformView _platformView;
    private readonly InputHandler _inputHandler;
    
    [Inject]
    public PlatformPresenter(InputHandler inputHandler,  PlatformModel platformModel, PlatformView platformView)
    {
        _platformModel = platformModel;
        _platformView = platformView;
        _inputHandler = inputHandler;

        _inputHandler.OnInputDataUpdate += x => _platformModel.AddForce(x.HorizontalInputValue);
        _platformModel.OnHealthLose += UpdateHealth;
        _platformModel.OnHealthLose += _platformView.DrawHealth;
    }

    private void UpdateHealth(int health)
    {
        if (health > 0)
            return;
        
        OnLose?.Invoke();
    }


    public void Dispose()
    {
        _platformModel.OnHealthLose -= UpdateHealth;
        _platformModel.OnHealthLose -= _platformView.DrawHealth;
    }
}
