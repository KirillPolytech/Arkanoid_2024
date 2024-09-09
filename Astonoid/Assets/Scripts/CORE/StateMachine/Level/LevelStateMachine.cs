using System;
using System.Linq;
using Arkanoid.InputSystem;
using Arkanoid.Settings;
using Arkanoid.StateMachine;
using UnityEngine;
using Zenject;

public class LevelStateMachine : StateMachine, IDisposable, IInitializable
{
    private readonly InputTypeController _inputTypeController;
    private readonly LoseTrigger _loseTrigger;
    private readonly HealthPresenter _healthPresenter;
    private readonly BallPool _ballPool;
    private readonly BlockService _blockService;

    private State _lastState;

    [Inject]
    public LevelStateMachine(
        TimeFreezer timeFreezer,
        LevelWindowController levelWindowController,
        InputTypeController inputTypeController,
        Settings settings,
        LoseTrigger loseTrigger,
        HealthPresenter healthPresenter,
        BlockPool blockPool,
        BallPool ballPool,
        BlockService blockService,
        UserData userData,
        LevelTimer levelTimer,
        Transform ballDefaultPos)
    {
        _states.Add(new InitialState(blockPool, ballPool, ballDefaultPos, levelWindowController, timeFreezer, healthPresenter, levelTimer));
        _states.Add(new BeginState(levelWindowController, ballPool, settings, ballDefaultPos));
        _states.Add(new PauseState(timeFreezer, levelWindowController));
        _states.Add(new ContinueState(timeFreezer, levelWindowController));
        _states.Add(new ResetState(ballPool, ballDefaultPos, levelWindowController, timeFreezer, levelTimer));
        _states.Add(new LoseState(timeFreezer, levelWindowController));
        _states.Add(new WinState(timeFreezer, levelWindowController, userData, levelTimer));

        _inputTypeController = inputTypeController;
        _healthPresenter = healthPresenter;
        _loseTrigger = loseTrigger;
        _ballPool = ballPool;
        _blockService = blockService;

        _inputTypeController.CurrentInputHandler.OnInputDataUpdate += HandlePauseWindow;
        _inputTypeController.CurrentInputHandler.OnInputDataUpdate += BeginGame;
        _healthPresenter.OnHealthLose += HandleHealthCount;
        _loseTrigger.OnBallEnter += HandleBallCount;
        _blockService.OnBlockDestruct += HandleActiveBlockCount;
    }

    public void Initialize()
    {
        SetState<InitialState>();
    }

    public override void SetState<T>()
    {
        State state = _states.FirstOrDefault(state => state.GetType() == typeof(T));

        if (state == null)
            throw new Exception("Unknown state");

        CurrentState?.ExitState();

        CurrentState = state;

        CurrentState.EnterState();
    }

    private void BeginGame(InputData inputData)
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1))
            //SetState<WinState>();
        
        if (!inputData.IsStartGameButtonPressed)
            return;

        if (CurrentState.GetType() != typeof(InitialState) 
            && CurrentState.GetType() != typeof(ResetState))
            return;

        SetState<BeginState>();
    }

    private void HandlePauseWindow(InputData data)
    {
        if (!data.EscapePressed
            || CurrentState.GetType() == typeof(WinState)
            || CurrentState.GetType() == typeof(LoseState))
            return;

        if (_lastState != null)
        {
            SetState<ContinueState>();
            _lastState = null;
            return;
        }

        SetState<PauseState>();

        _lastState = CurrentState;
    }

    private void HandleHealthCount(int health)
    {
        if (health > 0)
            return;

        SetState<LoseState>();
    }

    private void HandleBallCount()
    {
        if (_ballPool.GetActive().Count > 0)
            return;

        SetState<ResetState>();
        _healthPresenter.LoseHealth();
    }

    private void HandleActiveBlockCount(int count)
    {
        if (count > 1)
            return;

        SetState<WinState>();
    }

    public void Dispose()
    {
        _inputTypeController.CurrentInputHandler.OnInputDataUpdate -= HandlePauseWindow;
        _inputTypeController.CurrentInputHandler.OnInputDataUpdate -= BeginGame;
        _healthPresenter.OnHealthLose -= HandleHealthCount;
        _loseTrigger.OnBallEnter -= _healthPresenter.LoseHealth;
        _blockService.OnBlockDestruct -= HandleActiveBlockCount;
    }
}