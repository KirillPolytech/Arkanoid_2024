using Arkanoid.StateMachine;
using UnityEngine;

public class ResetState : State
{
    private readonly BallPool _ballPool;
    private readonly Transform _ballDefaultPos;
    private readonly WindowController _windowController;
    private readonly TimeFreezer _timeFreezer;
    private readonly PlatformPresenter _platformPresenter;
    private readonly LevelTimer _levelTimer;
    
    public ResetState(
        BallPool ballPool, 
        Transform ballDefaultPos, 
        WindowController windowController, 
        TimeFreezer timeFreezer, 
        LevelTimer levelTimer)
    {
        _ballDefaultPos = ballDefaultPos;
        _ballPool = ballPool;
        _windowController = windowController;
        _timeFreezer = timeFreezer;
        _levelTimer = levelTimer;
    }
    
    public override void EnterState()
    {
        _levelTimer.Reset();
        _timeFreezer.UnFreeze();
        _windowController.Open<GamePlayWindow>();
        _ballPool.Reset();
        _ballPool.Pop(_ballDefaultPos.position);
    }

    public override void ExitState()
    {
    }
}
