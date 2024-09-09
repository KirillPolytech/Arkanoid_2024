using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

public class BootContext : MonoInstaller
{
    [Space(15)] [SerializeField] private SceneLoader sceneLoader;
    [Space(15)] [SerializeField] private AudioMixer audioMixer;
    [Space(15)] [SerializeField] private MusicPlayer musicPlayer;
    [Space(15)] [SerializeField] private TextMeshProUGUI fpsCounter;
    [Space(15)] [SerializeField] private FPSSO fpsso;

    public override void InstallBindings()
    {
        Container.Bind<UserData>().AsSingle();
        Container.Bind<MouseSensivity>().AsSingle();
        
        Container.BindInstance(sceneLoader).AsSingle();

        Container.BindInterfacesAndSelfTo<VolumeSettings>().AsSingle().WithArguments(audioMixer);

        Container.BindInstance(musicPlayer).AsSingle();

        Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();

        Container.BindInstance(fpsso).AsSingle();

        BindFPS();
    }

    private void BindFPS()
    {
        Container.BindInterfacesAndSelfTo<FPSView>().AsSingle().WithArguments(fpsCounter);
        Container.BindInterfacesAndSelfTo<FPSPresenter>().AsSingle();
    }
}