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
        _platformPresenter.ReduceSize();
    }
}
