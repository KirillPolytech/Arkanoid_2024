using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class DivisionBallsBuff : Buff
{
    private readonly BallPool _ballPool;

    public DivisionBallsBuff(
        CompositeDisposable compositeDisposable,
        BallPool ballPool,
        Collider buffCol) : base(buffCol.gameObject)
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
            Ball temp = _ballPool.Pop();

            if (temp == null)
                break;

            Vector3 pos = balls[i].Rb.position;
            Vector3 velocity = 
                (Vector3.up * Random.Range(-1, 2f) + Vector3.right * Random.Range(-1, 2f)).normalized *
                    balls[i].Rb.velocity.magnitude;
            temp.Initialize(velocity, pos);
        }

        _buffGameObject.SetActive(false);
    }
}