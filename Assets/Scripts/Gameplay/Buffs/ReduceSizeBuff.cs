using Zenject;

public class ReduceSizeBuff : Buff
{
    private PlatformPresenter _platformPresenter;
    
    [Inject]
    public void Construct(PlatformPresenter platformPresenter)
    {
        _platformPresenter = platformPresenter;
    }
    
    public override void Execute()
    {
        if (!gameObject.activeSelf)
            return;
        
        _platformPresenter.ReduceSize();
        gameObject.SetActive(false);
    }
}
