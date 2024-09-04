using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

public class LoseTrigger : IDisposable
{
    private readonly CompositeDisposable  _disposables = new CompositeDisposable ();

    public Action OnBallEnter;

    private Collider _collider;

    [Inject]
    public void Construct(Collider col)
    {
        _collider = col;

        _collider.OnCollisionEnterAsObservable().Subscribe(c =>
        {
            if (!c.gameObject.CompareTag(TagStorage.BallTag))
                return;

            OnBallEnter?.Invoke();
        }).AddTo(_disposables);
    }


    public void Dispose()
    {
        _disposables.Clear();
    }
}