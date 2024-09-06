using System;
using System.Collections;
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

    private Vector3 _initalPosition;

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

        _initalPosition = _collider.transform.position;
    }

    private void HandleCollision(Collision collision)
    {
        if (!collision.gameObject.CompareTag(TagStorage.BuffTag))
            return;
        
        collision.gameObject.GetComponent<Buff>().Execute();
    }

    public void Resize(float koeff, float duration)
    {
        if (_collider.transform.localScale != Vector3.one)
            return;
        
        Resizing(koeff, duration).ToObservable().Subscribe().AddTo(_disposables);
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

        _disposables.Clear();
    }
}