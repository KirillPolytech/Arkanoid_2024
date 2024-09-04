using Arkanoid.StateMachine;
using UnityEngine;

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

        Cursor.lockState = CursorLockMode.Confined;
    }

    public override void ExitState()
    {
        _timeFreezer.UnFreeze();
    }
}