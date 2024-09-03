namespace Arkanoid.StateMachine
{
    public abstract class StateMachine
    {
        public State CurrentState { get; protected set; }

        public abstract void SetState<T>() where T : State;
    }
}