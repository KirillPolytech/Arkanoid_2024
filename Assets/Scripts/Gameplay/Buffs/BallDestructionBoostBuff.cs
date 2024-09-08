using System.Collections.Generic;
using Arkanoid.Settings;
using UnityEngine;
using Zenject;

public class BallDestructionBoostBuff : Buff
{
    private readonly BallPool _ballPool;
    private readonly Settings _settings;
    
    [Inject]
    public BallDestructionBoostBuff(
        Collider buffCol, 
        BallPool ballPool,
        Settings settings) : base(buffCol.gameObject)
    {
        _ballPool = ballPool;
        _settings = settings;
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
