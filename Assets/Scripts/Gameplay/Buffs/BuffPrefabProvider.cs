using Zenject;

public class BuffPrefabProvider : IDataProvider<BuffData>
{
    private readonly BuffData[] _buffs;
    
    [Inject]
    public BuffPrefabProvider(BuffData[] buffs)
    {
        _buffs = buffs;
    }

    public BuffData[] GetArray() => _buffs;
}
