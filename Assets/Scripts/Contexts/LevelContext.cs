using Arkanoid.InputSystem;
using TMPro;
using UnityEngine;
using Zenject;
using Arkanoid.Settings;

public class LevelContext : MonoInstaller
{
    [SerializeField] private Rigidbody platformRb;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private PlatformPresenter platformPresenter;
    
    [SerializeField] private LevelWindowController levelWindowController;
    
    [SerializeField] private Settings settings;

    public override void InstallBindings()
    {
        Container.BindInstance(settings).AsSingle();
        
        Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle();
        Container.Bind<TimeFreezer>().AsSingle();
        Container.BindInstance(levelWindowController).AsSingle();

        Container.Bind<PlatformModel>().AsSingle().WithArguments(platformRb);
        Container.BindInstance(platformPresenter).AsSingle();
        
        Container.Bind<HealthView>().AsSingle().WithArguments(healthText);
        Container.Bind<Health>().AsSingle();
        
        Container.Bind<LevelStateMachine>().AsSingle().NonLazy();
    }
}