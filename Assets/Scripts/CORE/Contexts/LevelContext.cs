using Arkanoid.InputSystem;
using TMPro;
using UnityEngine;
using Zenject;
using Arkanoid.Settings;
using UniRx;
using UnityEngine.Localization;

public class LevelContext : MonoInstaller
{
    [SerializeField] private Camera cam;

    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private Collider[] blocks;

    [SerializeField] private Rigidbody platformRb;
    [SerializeField] private Collider platformCollider;

    [Space(15)] [SerializeField] private BuffData[] buffPrefabs;

    [Space(15)] [SerializeField] private Rigidbody ballPrefab;
    [SerializeField] private Transform ballDefaultPos;

    [Space(15)] [SerializeField] private Collider loseTrigger;

    [Space(15)] [SerializeField] private TextMeshProUGUI healthText;

    [Space(15)] [SerializeField] private LevelWindowController levelWindowController;

    [Space(15)] [SerializeField] private Settings settings;
    
    [Header("Sounds")]
    [Space(15)] [SerializeField] private AudioSource ballHitSound;
    
    [Header("Localization")]
    [Space(15)] [SerializeField] private LocalizedString localizedHealth;
    
    public override void InstallBindings()
    {
        Container.Bind<CompositeDisposable>().AsSingle();
        
        Container.Bind<PlayerCamera>().AsSingle().WithArguments(cam);

        Container.Bind<Arkanoid.Factory>().AsSingle();

        Container.BindInstance(settings).AsSingle();

        BindInput();
        
        Container.Bind<TimeFreezer>().AsSingle();
        Container.BindInstance(levelWindowController).AsSingle();
        
        Container.BindInterfacesAndSelfTo<HitSoundPlayer>().AsSingle().WithArguments(ballHitSound);

        BindProviders();
        BindPools();
        BindPlatform();
        BindHealth();
        
        Container.Bind<LoseTrigger>().AsSingle().WithArguments(loseTrigger);

        Container.BindInterfacesAndSelfTo<LevelStateMachine>().AsSingle().WithArguments(ballDefaultPos);

        Container.BindInterfacesAndSelfTo<BlockService>().AsSingle();
    }

    private void BindHealth()
    {
        Container.Bind<HealthView>().AsSingle().WithArguments(healthText, localizedHealth);
        Container.Bind<Health>().AsSingle();
        Container.BindInterfacesAndSelfTo<HealthPresenter>().AsSingle();
    }

    private void BindPlatform()
    {
        Container.Bind<PlatformModel>().AsSingle().WithArguments(platformRb);
        Container.BindInterfacesAndSelfTo<PlatformPresenter>().AsSingle().WithArguments(platformCollider);
    }

    private void BindProviders()
    {
        Container.Bind<BlockProvider>().AsSingle().WithArguments(blocks);
        Container.BindInstance(new BuffPrefabProvider(buffPrefabs)).AsSingle();
        Container.Bind<BuffPoolInstantiator>().AsSingle();
        Container.Bind<BuffPoolsProvider>().AsSingle();
    }

    private void BindPools()
    {
        Container.BindInterfacesAndSelfTo<BallPool>().AsSingle().WithArguments(ballPrefab);
        Container.Bind<BlockPool>().AsSingle().WithArguments(blockPrefab);
    }

    private void BindInput()
    {
        Container.BindInterfacesAndSelfTo<KeyboardInputHandler>().AsSingle();
        Container.BindInterfacesAndSelfTo<MouseInputHandler>().AsSingle();
        Container.BindInterfacesAndSelfTo<InputTypeController>().AsSingle();
    }
}