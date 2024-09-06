using Arkanoid;

public class BuffPoolsProvider : IDataProvider<Pool<Buff>>
{
    private readonly Pool<Buff>[] _pools;

    public BuffPoolsProvider(BuffPrefabProvider buffPrefabProvider, Factory factory)
    {
        Buff[] arr = buffPrefabProvider.GetArray();
        _pools = new Pool<Buff>[arr.Length];
        for (int i = 0; i < arr.Length; i++)
        {
            _pools[i] = new Pool<Buff>(arr[i].gameObject, factory);
        }
    }

    public Pool<Buff>[] GetArray() => _pools;
}