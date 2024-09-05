using System;
using System.Linq;
using Arkanoid.StateMachine;
using UnityEngine;

public class GameStateMachine : StateMachine, IDisposable
{
    private readonly SceneLoader _sceneLoader;
    private MusicPlayer _musicPlayer;
    
    private GameStateMachine(SceneLoader sceneLoader, MusicPlayer musicPlayer)
    {
        _sceneLoader = sceneLoader;
        _musicPlayer = musicPlayer;
        
        _states.Add(new LevelState());

        sceneLoader.OnEndLoading += ChangeStateBySceneName;
    }

    public override void SetState<T>()
    {
        State state = _states.FirstOrDefault(state => state.GetType() == typeof(T));

        if (state == null)
            throw new Exception("Unknown state");

        CurrentState?.ExitState();

        CurrentState = state;

        CurrentState.EnterState();
    }

    private void ChangeStateBySceneName(string sceneName)
    {
        var contains = sceneName.Contains(SceneNameStorage.LevelName);

        switch (contains)
        {
            case true:
                Cursor.lockState = CursorLockMode.Confined;
                _musicPlayer.ChangeAudioClip(sceneName);
                break;
            case false:
                _musicPlayer.ChangeAudioClip(sceneName);
                break;
            default:
                throw new Exception($"Unknown scene: {sceneName}");
        }
    }


    public void Dispose()
    {
        _sceneLoader.OnEndLoading += ChangeStateBySceneName;
    }
}