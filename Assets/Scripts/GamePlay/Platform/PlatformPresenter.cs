using System;
using System.Collections;
using Arkanoid.InputSystem;
using UnityEngine;
using Zenject;
using Arkanoid.Settings;
using UniRx;

public class PlatformPresenter : IDisposable
{
    private PlatformModel _platformModel;
    private InputHandler _inputHandler;
    private Settings _settings;
    private CompositeDisposable _compositeDisposable;

    private Action<InputData> _cachedAddForce;

    private Collider _collider;

    private Vector3 _initalPosition;

    [Inject]
    public void Construct(
        Collider col,
        InputHandler inputHandler,
        PlatformModel platformModel,
        Settings settings,
        CompositeDisposable compositeDisposable)
    {
        _platformModel = platformModel;
        _inputHandler = inputHandler;
        _settings = settings;
        _collider = col;
        _compositeDisposable = compositeDisposable;

        _cachedAddForce = x => 
            _platformModel.AddForce(x.HorizontalInputValue * _settings.PlatformSpeed);
        
        _inputHandler.OnInputDataUpdateFixed += _cachedAddForce;

        _initalPosition = _collider.transform.position;
    }

    public void Resize(float koeff, float duration)
    {
        if (_collider.transform.localScale != Vector3.one)
            return;
        
        Resizing(koeff, duration).ToObservable().Subscribe().AddTo(_compositeDisposable);
    }

    private IEnumerator Resizing(float koeff, float duration)
    {
        _collider.transform.localScale /= koeff;
        yield return new WaitForSeconds(duration);
        _collider.transform.localScale = Vector3.one;
    }

    public void Reset()
    {
        _collider.transform.position = _initalPosition;
    }

    public void Dispose()
    {
        _inputHandler.OnInputDataUpdate -= _cachedAddForce;
    }
}