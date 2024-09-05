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

    private State _lastState;

    [Inject]
    public LevelStateMachine(
        TimeFreezer timeFreezer,
        LevelWindowController levelWindowController,
        InputHandler inputHandler,
        Settings settings,
        LoseTrigger loseTrigger,
        HealthPresenter healthPresenter,
        BallPool ballPool,
        Transform ballDefaultPos)
    {
        _states.Add(new PauseState(timeFreezer, levelWindowController));
        _states.Add(new PlayState(levelWindowController, ballPool, settings, ballDefaultPos));
        _states.Add(new InitialState(ballPool, ballDefaultPos));

        _inputHandler = inputHandler;
        _healthPresenter = healthPresenter;
        _loseTrigger = loseTrigger;
        
        _inputHandler.OnInputDataUpdate += HandlePauseWindow;
        _inputHandler.OnInputDataUpdate += BeginGame;
        loseTrigger.OnBallEnter += healthPresenter.LoseHealth;
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
        if (inputData.HorizontalInputValue != 0 || !inputData.IsLMBPressed)
            return;
        
        if (CurrentState.GetType() != typeof(InitialState))
            return;

        SetState<PlayState>();
    }

    private void HandlePauseWindow(InputData data)
    {
        if (!data.EscapePressed || CurrentState.GetType() == typeof(WinState))
            return;

        if (_lastState != null)
        {
            SetState<PlayState>();
            _lastState = null;
            return;
        }

        SetState<PauseState>();

        _lastState = CurrentState;
    }

    public void Dispose()
    {
        _inputHandler.OnInputDataUpdate -= HandlePauseWindow;
        _inputHandler.OnInputDataUpdate -= BeginGame;
        _loseTrigger.OnBallEnter -= _healthPresenter.LoseHealth;
    }
}