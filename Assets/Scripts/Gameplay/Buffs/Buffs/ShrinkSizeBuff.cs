using Arkanoid.Settings;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

public class ShrinkSizeBuff : Buff
{
    private readonly PlatformPresenter _platformPresenter;
    private readonly Settings _settings;
    
    [Inject]
    public ShrinkSizeBuff(
        Collider buffCol, 
        PlatformPresenter platformPresenter, 
        Settings settings) : base(buffCol.gameObject)
    {
        _platformPresenter = platformPresenter;
        _settings = settings;
    }
    
    public override void Execute()
    {
        if (!_buffGameObject.activeSelf)
            return;
        
        _platformPresenter.Resize(_settings.BuffCoeff, _settings.BuffDuration);
        _buffGameObject.SetActive(false);
    }
}
