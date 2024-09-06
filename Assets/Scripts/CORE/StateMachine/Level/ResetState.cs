using Arkanoid.StateMachine;
using UnityEngine;

public class ResetState : State
{
    private readonly BallPool _ballPool;
    private readonly Transform _ballDefaultPos;
    private readonly WindowController _windowController;
    private readonly TimeFreezer _timeFreezer;
    private readonly PlatformPresenter _platformPresenter;
    
    private Ball _ball;
    
    public ResetState(
        BallPool ballPool, 
        Transform ballDefaultPos, 
        WindowController windowController, 
        TimeFreezer timeFreezer,
        PlatformPresenter platformPresenter)
    {
        _ballDefaultPos = ballDefaultPos;
        _ballPool = ballPool;
        _windowController = windowController;
        _timeFreezer = timeFreezer;
        _platformPresenter = platformPresenter;
    }
    
    public override void EnterState()
    {
        _timeFreezer.UnFreeze();
        _windowController.Open<GamePlayWindow>();
        _ballPool.Reset();
        _ball = _ballPool.Pop();
        _platformPresenter.Reset();
        
        _ball.SetPosition(_ballDefaultPos.position);
    }

    public override void ExitState()
    {
    }
}
