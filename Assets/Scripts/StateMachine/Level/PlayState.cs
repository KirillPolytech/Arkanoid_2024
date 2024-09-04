using Arkanoid.StateMachine;
using UnityEngine;

public class PlayState : State
{
    private readonly LevelWindowController _windowController;
    
    public PlayState(LevelWindowController windowController)
    {
        _windowController = windowController;
    }
    
    public override void EnterState()
    {
        _windowController.Open<GamePlayWindow>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void ExitState()
    {
        
    }
}
