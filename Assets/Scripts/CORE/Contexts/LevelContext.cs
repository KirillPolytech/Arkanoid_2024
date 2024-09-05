using Arkanoid.InputSystem;
using TMPro;
using UnityEngine;
using Zenject;
using Arkanoid.Settings;

public class LevelContext : MonoInstaller
{
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private Collider[] blocks;
    
    [SerializeField] private Rigidbody platformRb;
    [SerializeField] private Collider platformCollider;
    
    [Space(15)][SerializeField] private Ball ballPrefab;
    [SerializeField] private Transform ballDefaultPos;
    
    [Space(15)][SerializeField] private Collider loseTrigger;
    
    [Space(15)][SerializeField] private TextMeshProUGUI healthText;
    
    [Space(15)][SerializeField] private LevelWindowController levelWindowController;
    
    [Space(15)][SerializeField] private Settings settings;

    public override void InstallBindings()
    {
        Container.Bind<Arkanoid.Factory>().AsSingle();
        
        Container.BindInstance(settings).AsSingle();
        
        Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle();
        Container.Bind<TimeFreezer>().AsSingle();
        Container.BindInstance(levelWindowController).AsSingle();
        
        Container.Bind<BallPool>().AsSingle().WithArguments(ballPrefab.gameObject);
        Container.Bind<BlockPool>().AsSingle().WithArguments(blockPrefab, new BlockProvider(blocks));

        Container.Bind<PlatformModel>().AsSingle().WithArguments(platformRb);
        Container.BindInterfacesAndSelfTo<PlatformPresenter>().AsSingle().WithArguments(platformCollider);
        
        Container.Bind<HealthView>().AsSingle().WithArguments(healthText);
        Container.Bind<Health>().AsSingle();
        Container.BindInterfacesAndSelfTo<HealthPresenter>().AsSingle();

        Container.Bind<LoseTrigger>().AsSingle().WithArguments(loseTrigger);
        
        Container.BindInterfacesAndSelfTo<LevelStateMachine>().AsSingle().WithArguments(ballDefaultPos);
    }
}