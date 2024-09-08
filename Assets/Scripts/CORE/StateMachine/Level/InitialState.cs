using Arkanoid.StateMachine;
using UnityEngine;

public class InitialState : State
{
    private readonly BlockPool _blockPool;
    private readonly BallPool _ballPool;
    private readonly Transform _ballDefaultPos;
    private readonly WindowController _windowController;
    private readonly TimeFreezer _timeFreezer;
    private readonly HealthPresenter _healthPresenter;
    
    private Ball _ball;
    
    public InitialState(
        BlockPool blockPool,
        BallPool ballPool, 
        Transform ballDefaultPos, 
        WindowController windowController, 
        TimeFreezer timeFreezer,
        HealthPresenter healthPresenter)
    {
        _ballDefaultPos = ballDefaultPos;
        _ballPool = ballPool;
        _blockPool = blockPool;
        _windowController = windowController;
        _timeFreezer = timeFreezer;
        _healthPresenter = healthPresenter;
    }
    
    public override void EnterState()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
        _timeFreezer.UnFreeze();
        _windowController.Open<GamePlayWindow>();
        _blockPool.ActivateAll();
        _ballPool.Reset();
        _ball = _ballPool.Pop();
        _healthPresenter.Reset();
        
        _ball.SetPosition(_ballDefaultPos.position);
    }

    public override void ExitState()
    {
    }
}
