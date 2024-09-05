using UnityEngine;
using UnityEngine.Audio;
using Zenject;

public class BootContext : MonoInstaller
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private MusicPlayer musicPlayer;
    
    public override void InstallBindings()
    {
        Container.BindInstance(sceneLoader).AsSingle();
        
        Container.BindInterfacesAndSelfTo<VolumeSettings>().AsSingle().WithArguments(audioMixer);
        
        Container.BindInstance(musicPlayer).AsSingle();

        Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle().NonLazy();
    }
}
