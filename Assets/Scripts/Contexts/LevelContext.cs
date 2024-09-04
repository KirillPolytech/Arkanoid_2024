using Arkanoid.InputSystem;
using TMPro;
using UnityEngine;
using Zenject;
using Arkanoid.Settings;

public class LevelContext : MonoInstaller
{
    [SerializeField] private Rigidbody platformRb;
    [SerializeField] private Collider platformCollider;
    
    [SerializeField] private Collider loseTrigger;
    
    [SerializeField] private TextMeshProUGUI healthText;
    
    [SerializeField] private LevelWindowController levelWindowController;
    
    [SerializeField] private Settings settings;

    public override void InstallBindings()
    {
        Container.BindInstance(settings).AsSingle();
        
        Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle();
        Container.Bind<TimeFreezer>().AsSingle();
        Container.BindInstance(levelWindowController).AsSingle();

        Container.Bind<PlatformModel>().AsSingle().WithArguments(platformRb);
        Container.BindInterfacesAndSelfTo<PlatformPresenter>().AsSingle().WithArguments(platformCollider);
        
        Container.Bind<HealthView>().AsSingle().WithArguments(healthText);
        Container.Bind<Health>().AsSingle();

        Container.Bind<LoseTrigger>().AsSingle().WithArguments(loseTrigger);
        
        Container.Bind<LevelStateMachine>().AsSingle().NonLazy();
    }
}