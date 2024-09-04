using System;
using Arkanoid.InputSystem;
using UnityEngine;
using Zenject;
using Arkanoid.Settings;

public class PlatformPresenter : MonoBehaviour
{
    private PlatformModel _platformModel;
    private InputHandler _inputHandler;
    private Settings _settings;

    private Action<InputData> _cachedAddForce;

    [Inject]
    public void Construct(
        InputHandler inputHandler, 
        PlatformModel platformModel,
        Settings settings)
    {
        _platformModel = platformModel;
        _inputHandler = inputHandler;
        _settings = settings;

        _cachedAddForce = x => _platformModel.AddForce(x.HorizontalInputValue * _settings.PlatformSpeed);

        _inputHandler.OnInputDataUpdateFixed += _cachedAddForce;
    }

    private void OnDisable()
    {
        _inputHandler.OnInputDataUpdate -= _cachedAddForce;
    }
}