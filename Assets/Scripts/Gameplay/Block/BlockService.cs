using System;
using System.Linq;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class BlockService : IInitializable, IDisposable
{
    private readonly CompositeDisposable _disposables = new CompositeDisposable();

    public Action<int> OnBlockHit;
    
    private Pool<Buff> _divisionBallPool;
    private Pool<Buff> _reduceSizePool;
    private Pool<Buff> _expandSizePool;
    private Collider[] _blocks;

    [Inject]
    public void Construct(Arkanoid.Factory factory, 
        BlockProvider dataProvider,
        Buff divisionBallPrefab,
        Buff reduceSizePrefab,
        Buff expandSizePrefab)
    {
        _blocks = dataProvider.GetArray();

        _divisionBallPool = new Pool<Buff>(divisionBallPrefab.gameObject, factory);
        _reduceSizePool = new Pool<Buff>(reduceSizePrefab.gameObject, factory);
        _expandSizePool = new Pool<Buff>(expandSizePrefab.gameObject, factory);
    }

    public void Initialize()
    {
        foreach (var block in _blocks)
        {
            block.OnCollisionEnterAsObservable().Subscribe(col =>
            {
                HandleCollision(col, block);
            }).AddTo(_disposables);
        }
    }

    private void HandleCollision(Collision col, Collider block)
    {
        if (!col.gameObject.CompareTag(TagStorage.BallTag))
            return;
        
        OnBlockHit?.Invoke(_blocks.Count(x => x.gameObject.activeSelf));
                
        Buff obj = null;
        int rand = Random.Range(0, 10);
        obj = rand switch
        {
            0 => _divisionBallPool.Pop(),
            1 => _reduceSizePool.Pop(),
            2 => _expandSizePool.Pop(),
            _ => obj
        };
        if (obj)
            obj.transform.SetPositionAndRotation(block.transform.position, Quaternion.identity);
        block.gameObject.SetActive(false);
    }

    public void Dispose()
    {
        _disposables?.Dispose();
    }
}