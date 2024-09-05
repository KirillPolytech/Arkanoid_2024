using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class BallPool : Pool<Ball>
{
    [Inject]
    public BallPool(GameObject prefab, Arkanoid.Factory factory) : base(prefab, factory, DefaultAmount)
    {
        
    }

    public Ball[] GetActiveBalls()
    {
        return _pool.Where(x => x.GameObject().activeSelf).ToArray();
    }
}
