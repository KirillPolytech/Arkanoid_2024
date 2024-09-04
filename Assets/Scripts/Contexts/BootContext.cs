using UnityEngine;
using Zenject;

public class BootContext : MonoInstaller
{
    [SerializeField] private SceneLoader sceneLoader;
    
    public override void InstallBindings()
    {
        Container.BindInstance(sceneLoader).AsSingle();
    }
}
