using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

public class BootContext : MonoInstaller
{
    [Space(15)][SerializeField] private SceneLoader sceneLoader;
    [Space(15)][SerializeField] private AudioMixer audioMixer;
    [Space(15)][SerializeField] private MusicPlayer musicPlayer;
    [Space(15)] [SerializeField] private TextMeshProUGUI fpsCounter;
    
    public override void InstallBindings()
    {
        Container.BindInstance(sceneLoader).AsSingle();
        
        Container.BindInterfacesAndSelfTo<VolumeSettings>().AsSingle().WithArguments(audioMixer);
        
        Container.BindInstance(musicPlayer).AsSingle();

        Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle().NonLazy();
        
        Container.BindInterfacesAndSelfTo<FPS>().AsSingle().WithArguments(fpsCounter);
    }
}
