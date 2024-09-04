using System.Collections.Generic;

namespace Arkanoid.StateMachine
{
    public abstract class StateMachine
    {
        protected readonly List<State> _states = new List<State>();

        public State CurrentState { get; protected set; }

        public abstract void SetState<T>() where T : State;
    }
}