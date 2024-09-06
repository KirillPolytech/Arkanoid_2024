using Arkanoid;
using UnityEngine;

public class BlockPool : Pool<Collider>
{
    public BlockPool(GameObject prefab, Factory factory, BlockProvider dataProvider) : base(prefab, factory, 0)
    {
        _pool.AddRange(dataProvider.GetArray());
    }

    public void ActivateAll()
    {
        foreach (var collider in _pool)
        {
            collider.gameObject.SetActive(true);
        }
    }
}
