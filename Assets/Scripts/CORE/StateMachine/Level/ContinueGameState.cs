using Arkanoid.StateMachine;
using UnityEngine;

public class ContinueGameState : State
{
    private readonly TimeFreezer _timeFreezer;
    private readonly LevelWindowController _levelWindowController;

    public ContinueGameState(TimeFreezer timeFreezer, LevelWindowController levelWindowController)
    {
        _timeFreezer = timeFreezer;
        _levelWindowController = levelWindowController;
    }
    
    public override void EnterState()
    {
        _levelWindowController.Open<GamePlayWindow>();
        _timeFreezer.UnFreeze();

        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void ExitState()
    {
        
    }
}
