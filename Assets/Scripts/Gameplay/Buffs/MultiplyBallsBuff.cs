using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MultiplyBallsBuff : Buff
{
    private BallPool _ballPool;
    
    [Inject]
    public void Construct(BallPool ballPool)
    {
        _ballPool = ballPool;
    }

    public override void Execute()
    {
        Ball[] balls = _ballPool.GetActiveBalls();
        List<Ball> clonedBalls = new List<Ball>();

        for (int i = 0; i < balls.Length; i++)
        {
            Ball temp = _ballPool.Pop();

            Vector3 pos = balls[i].Rb.position + balls[i].transform.right;
            Vector3 velocity = balls[i].Rb.velocity;
            temp.Initialize(velocity, pos);
            
            clonedBalls.Add(_ballPool.Pop());
        }
    }
}