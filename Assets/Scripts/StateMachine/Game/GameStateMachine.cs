using System;
using System.Linq;
using Arkanoid.StateMachine;

public class GameStateMachine : StateMachine
{
    private GameStateMachine()
    {
        _states.Add(new LevelState());
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
}