using Arkanoid.Settings;
using Zenject;

public class ShrinkSizeBuff : Buff
{
    private PlatformPresenter _platformPresenter;
    private Settings _settings;
    
    [Inject]
    public void Construct(PlatformPresenter platformPresenter, Settings settings)
    {
        _platformPresenter = platformPresenter;
        _settings = settings;
    }
    
    public override void Execute()
    {
        if (!gameObject.activeSelf)
            return;
        
        _platformPresenter.Resize(_settings.coefficient, _settings.buffDuration);
        gameObject.SetActive(false);
    }
}
