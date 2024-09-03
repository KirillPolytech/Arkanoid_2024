using Arkanoid.StateMachine;

public class PauseState : State
{
    private readonly TimeFreezer _timeFreezer;
    private readonly LevelWindowController _levelWindowController;

    public PauseState(TimeFreezer timeFreezer, LevelWindowController levelWindowController)
    {
        _timeFreezer = timeFreezer;
        _levelWindowController = levelWindowController;
    }

    public override void EnterState()
    {
        _timeFreezer.Freeze();
        _levelWindowController.Open<InGameMenu>();
    }

    public override void ExitState()
    {
        _timeFreezer.UnFreeze();
    }
}