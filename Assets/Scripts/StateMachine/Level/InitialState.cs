using Arkanoid.StateMachine;
using UnityEngine;

public class InitialState : State
{
    private readonly BallPool _ballPool;
    private readonly Transform _ballDefaultPos;
    
    private Ball _ball;
    
    public InitialState(BallPool ballPool, Transform ballDefaultPos)
    {
        _ballDefaultPos = ballDefaultPos;
        _ballPool = ballPool;
    }
    
    public override void EnterState()
    {
        _ballPool.Reset();
        _ball = _ballPool.Pop();
        
        _ball.SetPosition(_ballDefaultPos.position);
    }

    public override void ExitState()
    {
    }
}
