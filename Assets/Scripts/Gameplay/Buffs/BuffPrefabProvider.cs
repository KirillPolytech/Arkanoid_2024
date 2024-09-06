using Zenject;

public class BuffPrefabProvider : IDataProvider<Buff>
{
    private readonly Buff[] _pools;
    
    [Inject]
    public BuffPrefabProvider(Buff[] pools)
    {
        _pools = pools;
    }

    public Buff[] GetArray() => _pools;
}
