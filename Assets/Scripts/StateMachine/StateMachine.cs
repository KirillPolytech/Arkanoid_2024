using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    public State CurrentState { get; private set; }
    
    public void SetState<T>() where T : State
    {
        
    }
    
    public void ExitState<T>() where T : State
    {
        
    }
}
