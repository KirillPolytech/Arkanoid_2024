using Arkanoid.StateMachine;
using UnityEngine;

public class PlayState : State
{
    private readonly LevelWindowController _windowController;
    private readonly Ball _ball;
    
    public PlayState(LevelWindowController windowController, Ball ball)
    {
        _windowController = windowController;
        _ball = ball;
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
