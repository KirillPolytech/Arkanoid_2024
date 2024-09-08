using Arkanoid.Settings;
using UniRx;
using UnityEngine;
using Zenject;

public class ShrinkSizeBuff : Buff
{
    private readonly PlatformPresenter _platformPresenter;
    
    [Inject]
    public ShrinkSizeBuff(
        Collider buffCol, 
        PlatformPresenter platformPresenter, 
        Settings settings,
        CompositeDisposable compositeDisposable) : base(buffCol.gameObject, compositeDisposable, settings)
    {
        _platformPresenter = platformPresenter;
    }
    
    public override void Execute()
    {
        if (!_buffGameObject.activeSelf)
            return;
        
        _platformPresenter.Resize(_settings.BuffCoeff, _settings.BuffDuration);
        _buffGameObject.SetActive(false);
    }
}
