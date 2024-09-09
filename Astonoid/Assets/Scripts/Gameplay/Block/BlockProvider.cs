using UnityEngine;

public class BlockProvider : IDataProvider<Collider>
{
    private readonly Collider[] _colliders;
    
    public BlockProvider(Collider[] colliders)
    {
        _colliders = colliders;
    }

    public Collider[] GetArray() => _colliders;
}
