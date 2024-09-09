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
    private readonly LevelTimer _levelTimer;
    private readonly BlockService _blockService;
    
    public InitialState(
        BlockPool blockPool,
        BallPool ballPool, 
        Transform ballDefaultPos, 
        WindowController windowController, 
        TimeFreezer timeFreezer,
        HealthPresenter healthPresenter,
        LevelTimer levelTimer,
        BlockService blockService)
    {
        _ballDefaultPos = ballDefaultPos;
        _ballPool = ballPool;
        _blockPool = blockPool;
        _windowController = windowController;
        _timeFreezer = timeFreezer;
        _healthPresenter = healthPresenter;
        _levelTimer = levelTimer;
        _blockService = blockService;
    }
    
    public override void EnterState()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
        _blockService.Reset();
        _levelTimer.Reset();
        _ballPool.Reset();
        _ballPool.Pop(_ballDefaultPos.position);
        _timeFreezer.UnFreeze();
        _windowController.Open<GamePlayWindow>();
        _blockPool.ActivateAll();
        _healthPresenter.Reset();
    }

    public override void ExitState()
    {
    }
}
