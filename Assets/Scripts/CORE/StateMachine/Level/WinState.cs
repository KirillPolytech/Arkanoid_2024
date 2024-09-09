using System.Linq;
using Arkanoid.StateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinState : State
{
    private readonly TimeFreezer _timeFreezer;
    private readonly LevelWindowController _levelWindowController;
    private readonly UserData _userData;

    public WinState(TimeFreezer timeFreezer, 
        LevelWindowController levelWindowController,
        UserData userData)
    {
        _timeFreezer = timeFreezer;
        _levelWindowController = levelWindowController;
        _userData = userData;
    }
    
    public override void EnterState()
    {
        _timeFreezer.Freeze();
        _levelWindowController.Open<WinWindow>();
        Cursor.lockState = CursorLockMode.Confined;

        int ind = -1;
        for (int i = 0; i < SceneNameStorage.Levels.Length; i++)
        {
            if (SceneNameStorage.Levels[i] == SceneManager.GetActiveScene().name)
            {
                ind = i;
            }
        }

        _userData.ChangeLevelData(ind, new LevelData());
    }

    public override void ExitState()
    {
        
    }
}
