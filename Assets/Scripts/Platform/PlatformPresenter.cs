using System;
using Arkanoid.InputSystem;
using UnityEngine;
using Zenject;
using Arkanoid.Settings;

public class PlatformPresenter : MonoBehaviour
{
    public Action OnLose;

    private PlatformModel _platformModel;
    private PlatformView _platformView;
    private InputHandler _inputHandler;
    private Settings _settings;

    private Action<InputData> _cachedAddForce;

    [Inject]
    public void Construct(
        InputHandler inputHandler, 
        PlatformModel platformModel, 
        PlatformView platformView,
        Settings settings)
    {
        _platformModel = platformModel;
        _platformView = platformView;
        _inputHandler = inputHandler;
        _settings = settings;

        _cachedAddForce = x => _platformModel.AddForce(x.HorizontalInputValue * _settings.PlatformSpeed);

        _inputHandler.OnInputDataUpdate += _cachedAddForce;
        _platformModel.OnHealthLose += UpdateHealth;
        _platformModel.OnHealthLose += _platformView.DrawHealth;
    }

    private void UpdateHealth(int health)
    {
        if (health > 0)
            return;

        OnLose?.Invoke();
    }

    private void OnDisable()
    {
        _platformModel.OnHealthLose -= UpdateHealth;
        _platformModel.OnHealthLose -= _platformView.DrawHealth;
        _inputHandler.OnInputDataUpdate -= _cachedAddForce;
    }
}