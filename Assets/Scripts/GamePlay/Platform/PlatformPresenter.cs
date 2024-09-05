using System;
using Arkanoid.InputSystem;
using UnityEngine;
using Zenject;
using Arkanoid.Settings;
using UniRx;
using UniRx.Triggers;

public class PlatformPresenter : IDisposable
{
    private readonly CompositeDisposable _disposables = new CompositeDisposable();

    private PlatformModel _platformModel;
    private InputHandler _inputHandler;
    private Settings _settings;

    private Action<InputData> _cachedAddForce;

    private Collider _collider;

    [Inject]
    public void Construct(
        Collider col,
        InputHandler inputHandler,
        PlatformModel platformModel,
        Settings settings)
    {
        _platformModel = platformModel;
        _inputHandler = inputHandler;
        _settings = settings;
        _collider = col;

        _cachedAddForce = x => _platformModel.AddForce(x.HorizontalInputValue * _settings.PlatformSpeed);

        _collider.OnCollisionEnterAsObservable().Subscribe(HandleCollision).AddTo(_disposables);

        _inputHandler.OnInputDataUpdateFixed += _cachedAddForce;
    }

    private void HandleCollision(Collision collision)
    {
        if (!collision.gameObject.CompareTag(TagStorage.BuffTag))
            return;
        
        collision.gameObject.GetComponent<Buff>().Execute();
    }

    public void Dispose()
    {
        _inputHandler.OnInputDataUpdate -= _cachedAddForce;

        _disposables.Clear();
    }
}