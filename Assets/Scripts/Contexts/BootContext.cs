using UnityEngine;
using UnityEngine.Audio;
using Zenject;

public class BootContext : MonoInstaller
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private AudioMixer audioMixer;
    
    public override void InstallBindings()
    {
        Container.BindInstance(sceneLoader).AsSingle();
            
        Container.BindInterfacesAndSelfTo<VolumeSettings>().AsSingle().WithArguments(audioMixer).NonLazy();
    }
}
