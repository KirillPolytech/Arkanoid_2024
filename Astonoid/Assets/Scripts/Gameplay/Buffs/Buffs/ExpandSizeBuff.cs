using Arkanoid.Settings;
using UniRx;
using UnityEngine;
using Zenject;

public class ExpandSizeBuff : Buff
{
    private readonly PlatformPresenter _platformPresenter;

    [Inject]
    public ExpandSizeBuff(
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

        _platformPresenter.Resize(1 / _settings.BuffCoeff, _settings.BuffDuration);
        _buffGameObject.SetActive(false);
    }
}