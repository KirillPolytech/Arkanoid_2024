using System;
using System.Linq;
using Arkanoid.InputSystem;
using Arkanoid.Settings;
using Arkanoid.StateMachine;
using UnityEngine;
using Zenject;

public class LevelStateMachine : StateMachine, IDisposable, IInitializable
{
    private readonly InputHandler _inputHandler;
    private readonly LoseTrigger _loseTrigger;
    private readonly HealthPresenter _healthPresenter;
    private readonly BallPool _ballPool;
    private readonly BlockService _blockService;

    private State _lastState;

    [Inject]
    public LevelStateMachine(
        PlatformPresenter platformPresenter,
        TimeFreezer timeFreezer,
        LevelWindowController levelWindowController,
        InputHandler inputHandler,
        Settings settings,
        LoseTrigger loseTrigger,
        HealthPresenter healthPresenter,
        BlockPool blockPool,
        BallPool ballPool,
        BlockService blockService,
        Transform ballDefaultPos)
    {
        _states.Add(new InitialState(blockPool, ballPool, ballDefaultPos, levelWindowController, timeFreezer, healthPresenter));
        _states.Add(new BeginState(levelWindowController, ballPool, settings, ballDefaultPos));
        _states.Add(new PauseState(timeFreezer, levelWindowController));
        _states.Add(new ContinueState(timeFreezer, levelWindowController));
        _states.Add(new ResetState(ballPool, ballDefaultPos, levelWindowController, timeFreezer, platformPresenter));
        _states.Add(new LoseState(timeFreezer, levelWindowController));
        _states.Add(new WinState(timeFreezer, levelWindowController));

        _inputHandler = inputHandler;
        _healthPresenter = healthPresenter;
        _loseTrigger = loseTrigger;
        _ballPool = ballPool;
        _blockService = blockService;

        _inputHandler.OnInputDataUpdate += HandlePauseWindow;
        _inputHandler.OnInputDataUpdate += BeginGame;
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
        if (!inputData.IsSpacePressed)
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
        _inputHandler.OnInputDataUpdate -= HandlePauseWindow;
        _healthPresenter.OnHealthLose -= HandleHealthCount;
        _inputHandler.OnInputDataUpdate -= BeginGame;
        _loseTrigger.OnBallEnter -= _healthPresenter.LoseHealth;
        _blockService.OnBlockDestruct -= HandleActiveBlockCount;
    }
}