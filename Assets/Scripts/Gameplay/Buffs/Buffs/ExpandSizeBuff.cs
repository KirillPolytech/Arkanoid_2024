using Arkanoid.Settings;
using UnityEngine;
using Zenject;

public class ExpandSizeBuff : Buff
{
    private readonly PlatformPresenter _platformPresenter;
    private readonly Settings _settings;

    [Inject]
    public ExpandSizeBuff(
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

        _platformPresenter.Resize(1 / _settings.BuffCoeff, _settings.BuffDuration);
        _buffGameObject.SetActive(false);
    }
}