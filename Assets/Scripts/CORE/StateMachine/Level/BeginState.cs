using System.Linq;
using Arkanoid.Settings;
using Arkanoid.StateMachine;
using UnityEngine;

public class BeginState : State
{
    private readonly LevelWindowController _windowController;
    private readonly BallPool _ballPool;
    private readonly Settings _settings;
    private readonly Transform _initalPos;

    public BeginState(LevelWindowController windowController, BallPool ballPool, Settings settings, Transform initalPos)
    {
        _windowController = windowController;
        _ballPool = ballPool;
        _settings = settings;
        _initalPos = initalPos;
    }

    public override void EnterState()
    {
        _windowController.Open<GamePlayWindow>();

        Cursor.lockState = CursorLockMode.Locked;

        Vector3 velocity = (Vector3.up + Vector3.right * Random.Range(-1f, 2f)).normalized * _settings.BallStartForce;
        _ballPool.GetActive().FirstOrDefault()?.Initialize(velocity, _initalPos.position);
    }

    public override void ExitState()
    {
    }
}