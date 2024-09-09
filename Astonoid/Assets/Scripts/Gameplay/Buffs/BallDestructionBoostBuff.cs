using System.Collections.Generic;
using Arkanoid.Settings;
using UniRx;
using UnityEngine;
using Zenject;

public class BallDestructionBoostBuff : Buff
{
    private readonly BallPool _ballPool;
    
    [Inject]
    public BallDestructionBoostBuff(
        Collider buffCol, 
        BallPool ballPool,
        Settings settings,
        CompositeDisposable compositeDisposable) : base(buffCol.gameObject, compositeDisposable, settings)
    {
        _ballPool = ballPool;
    }
    
    public override void Execute()
    {
        if (!_buffGameObject.activeSelf)
            return;

        List<Ball> balls = _ballPool.GetActive();
        foreach (var ball in balls)
        {
            ball.BoostDamage(_settings.DamageBoost);
        }
        
        _buffGameObject.SetActive(false);
    }
}
