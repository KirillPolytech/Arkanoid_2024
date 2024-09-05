using System;
using System.Linq;
using Arkanoid.InputSystem;
using Arkanoid.StateMachine;
using UnityEngine;
using Zenject;

public class LevelStateMachine : StateMachine, IDisposable
{
    private readonly InputHandler _inputHandler;

    private State _lastState;

    [Inject]
    public LevelStateMachine(
        TimeFreezer timeFreezer,
        LevelWindowController levelWindowController,
        InputHandler inputHandler,
        BallPool ballPool,
        Transform ballDefaultPos)
    {
        _states.Add(new PauseState(timeFreezer, levelWindowController));
        _states.Add(new PlayState(levelWindowController, ballPool.GetActiveBalls()[0]));
        _states.Add(new InitialState(ballPool, ballDefaultPos));

        _inputHandler = inputHandler;
        _inputHandler.OnInputDataUpdate += HandlePauseWindow;
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

    private void HandlePauseWindow(InputData data)
    {
        if (!data.EscapePressed)
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
    }
}