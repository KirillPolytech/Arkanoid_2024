using System;
using System.Collections.Generic;
using System.Linq;
using Arkanoid.InputSystem;
using Arkanoid.StateMachine;
using Zenject;

public class LevelStateMachine : StateMachine, IDisposable
{
    private readonly InputHandler _inputHandler;

    private State _lastState;

    [Inject]
    public LevelStateMachine(
        TimeFreezer timeFreezer,
        LevelWindowController levelWindowController,
        InputHandler inputHandler)
    {
        _states.Add(new PauseState(timeFreezer, levelWindowController));
        _states.Add(new PlayState(levelWindowController));

        _inputHandler = inputHandler;
        _inputHandler.OnInputDataUpdate += CheckInputs;
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

    private void CheckInputs(InputData data)
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
        _inputHandler.OnInputDataUpdate -= CheckInputs;
    }
}