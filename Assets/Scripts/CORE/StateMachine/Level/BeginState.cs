using System.Linq;
using Arkanoid.Settings;
using Arkanoid.StateMachine;
using UnityEngine;

public class BeginState : State
{
    private readonly LevelWindowController _windowController;
    private readonly BallPool _ballPool;
    private readonly Settings _settings;
    private readonly Transform _initialPos;

    public BeginState(
        LevelWindowController windowController, 
        BallPool ballPool, 
        Settings settings, 
        Transform initialPos)
    {
        _windowController = windowController;
        _ballPool = ballPool;
        _settings = settings;
        _initialPos = initialPos;
    }

    public override void EnterState()
    {
        _windowController.Open<GamePlayWindow>();

        Cursor.lockState = CursorLockMode.Locked;

        Vector3 velocity = (Vector3.up + Vector3.right * Random.Range(-1f, 2f)).normalized * 
                           _settings.BallStartForce;
        _ballPool.GetActive().FirstOrDefault()?.Initialize(velocity, _initialPos.position);
    }

    public override void ExitState()
    {
    }
}