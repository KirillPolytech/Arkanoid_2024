using Arkanoid.StateMachine;

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
    }

    public override void ExitState()
    {
        
    }
}
