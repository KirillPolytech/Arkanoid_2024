using UnityEngine;
using Zenject;

public class BallPool : Pool<Ball>
{
    [Inject]
    public BallPool(GameObject prefab, Arkanoid.Factory factory) : base(prefab, factory, DefaultAmount)
    {
        
    }
}
