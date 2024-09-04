using System;
using Arkanoid.InputSystem;
using UnityEngine;
using Zenject;
using Arkanoid.Settings;
using UniRx;
using UniRx.Triggers;

public class PlatformPresenter : IDisposable
{
    private PlatformModel _platformModel;
    private InputHandler _inputHandler;
    private Settings _settings;

    private Action<InputData> _cachedAddForce;

    private Collider _collider;
    private readonly CompositeDisposable  _disposables = new CompositeDisposable ();

    private bool _canMove = true;

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

        _cachedAddForce = x => _platformModel.AddForce(x.HorizontalInputValue * _settings.PlatformSpeed, _canMove);

        _collider.OnCollisionEnterAsObservable().Subscribe(x =>
        {
            if (x.gameObject.CompareTag(TagStorage.BuffTag))
                x.gameObject.GetComponent<Buff>().Execute();
            
            if (x.gameObject.CompareTag(TagStorage.BallTag))
                _canMove = false;
        }).AddTo(_disposables);
        
        _collider.OnCollisionExitAsObservable().Subscribe(x =>
        {
            if (!x.gameObject.CompareTag(TagStorage.BallTag))
                return;

            _canMove = true;
        }).AddTo(_disposables);

        _inputHandler.OnInputDataUpdateFixed += _cachedAddForce;
    }

    public void Dispose()
    {
        _inputHandler.OnInputDataUpdate -= _cachedAddForce;

        _disposables.Clear();
    }
}