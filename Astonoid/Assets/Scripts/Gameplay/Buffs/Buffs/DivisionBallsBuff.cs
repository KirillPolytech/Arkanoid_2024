using System;
using System.Collections.Generic;
using Arkanoid.Settings;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Random = UnityEngine.Random;

public class DivisionBallsBuff : Buff
{
    private readonly BallPool _ballPool;

    public DivisionBallsBuff(
        CompositeDisposable compositeDisposable,
        BallPool ballPool,
        Collider buffCol,
        Settings settings) : base(buffCol.gameObject, compositeDisposable, settings)
    {
        _ballPool = ballPool;

        buffCol.OnCollisionEnterAsObservable().Subscribe(OnCollision).AddTo(compositeDisposable);
    }

    private void OnCollision(Collision col)
    {
        if (!col.gameObject.CompareTag(TagStorage.PlatformTag))
            return;

        Execute();
    }

    public override void Execute()
    {
        if (!_buffGameObject.activeSelf)
            return;

        List<Ball> balls = _ballPool.GetActive();

        int count = balls.Count;

        for (int i = 0; i < count; i++)
        {
            Vector3 pos = balls[i].Rb.position;

            Ball temp = _ballPool.Pop(pos);

            if (temp == null)
                break;

            Vector3 velocity = 
                (Vector3.up * Random.Range(-1, 2f) + Vector3.right * Random.Range(-1, 2f)).normalized *
                    balls[i].Rb.velocity.magnitude;
            temp.Initialize(velocity, pos);
        }

        _buffGameObject.SetActive(false);
    }
}