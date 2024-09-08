using System.Collections.Generic;
using UnityEngine;

public class BuffPoolsProvider : IDataProvider<Pool<Collider>>
{
    private readonly List<Pool<Collider>> _pools;

    public BuffPoolsProvider(
        BuffPoolInstantiator buffPoolInstantiator)
    {
        _pools = buffPoolInstantiator.GetPools();
    }

    public Pool<Collider>[] GetArray() => _pools.ToArray();
}