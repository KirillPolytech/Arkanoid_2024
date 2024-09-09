using Arkanoid.StateMachine;
using UnityEngine;

public class LoseState : State
{
    private readonly TimeFreezer _timeFreezer;
    private readonly LevelWindowController _levelWindowController;
    
    public LoseState(TimeFreezer timeFreezer, LevelWindowController levelWindowController)
    {
        _timeFreezer = timeFreezer;
        _levelWindowController = levelWindowController;
    }
    
    public override void EnterState()
    {
        _timeFreezer.Freeze();  
        _levelWindowController.Open<LoseWindow>();
        Cursor.lockState = CursorLockMode.Confined;
    }

    public override void ExitState()
    {
        
    }
}
