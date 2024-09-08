using System;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

public class LoseTrigger : IDisposable
{
    private readonly CompositeDisposable _disposables = new CompositeDisposable();

    public Action OnBallEnter;

    private Collider _collider;

    [Inject]
    public void Construct(Collider col, BallPool ballPool, BuffPoolsProvider buffPoolsProvider)
    {
        _collider = col;

        _collider.OnCollisionEnterAsObservable().Subscribe(collision =>
        {
            switch (collision.gameObject.tag)
            {
                case TagStorage.BallTag:
                    HandleBallCollision(ballPool, collision);
                    break;
                case TagStorage.BuffTag:
                    HandleBuffCollision(buffPoolsProvider.GetArray(), collision);
                    break;
            }
        }).AddTo(_disposables);
    }

    private void HandleBallCollision(BallPool ballPool, Collision collision)
    {
        ballPool.Push(collision.gameObject);
        OnBallEnter?.Invoke();
    }

    private void HandleBuffCollision(IReadOnlyList<Pool<Buff>> buffPools, Collision collision)
    {
        buffPools[0].Push(collision.gameObject);
    }

    public void Dispose()
    {
        _disposables.Clear();
    }
}