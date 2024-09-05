using Arkanoid.StateMachine;
using UnityEngine;

public class InitialState : State
{
    private readonly BlockPool _blockPool;
    private readonly BallPool _ballPool;
    private readonly Transform _ballDefaultPos;
    private readonly WindowController _windowController;
    private readonly TimeFreezer _timeFreezer;
    
    private Ball _ball;
    
    public InitialState(
        BlockPool blockPool,
        BallPool ballPool, 
        Transform ballDefaultPos, 
        WindowController windowController, 
        TimeFreezer timeFreezer)
    {
        _ballDefaultPos = ballDefaultPos;
        _ballPool = ballPool;
        _blockPool = blockPool;
        _windowController = windowController;
        _timeFreezer = timeFreezer;
    }
    
    public override void EnterState()
    {
        _timeFreezer.UnFreeze();
        _windowController.Open<GamePlayWindow>();
        _blockPool.ActivateAll();
        _ballPool.Reset();
        _ball = _ballPool.Pop();
        
        _ball.SetPosition(_ballDefaultPos.position);
    }

    public override void ExitState()
    {
    }
}
