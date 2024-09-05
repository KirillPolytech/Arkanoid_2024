using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class BlockService : IInitializable, IDisposable
{
    private readonly CompositeDisposable _disposables = new CompositeDisposable();
    
    private Pool<Buff> _multipleBallPool;
    private Pool<Buff> _reduceSizePool;
    private Collider[] _blocks;

    [Inject]
    public void Construct(Arkanoid.Factory factory, 
        BlockProvider dataProvider,
        Buff multipleBallPrefab,
        Buff reduceSizePrefab)
    {
        _blocks = dataProvider.GetArray();

        _multipleBallPool = new Pool<Buff>(multipleBallPrefab.gameObject, factory);
        _reduceSizePool = new Pool<Buff>(reduceSizePrefab.gameObject, factory);
    }

    public void Initialize()
    {
        foreach (var block in _blocks)
        {
            block.OnCollisionEnterAsObservable().Subscribe(_ =>
            {
                Buff obj = null;
                int rand = Random.Range(0, 2);
                obj = rand switch
                {
                    0 => _multipleBallPool.Pop(),
                    1 => _reduceSizePool.Pop(),
                    _ => obj
                };

                obj.transform.SetPositionAndRotation(block.transform.position, Quaternion.identity);
                block.gameObject.SetActive(false);
            }).AddTo(_disposables);
        }
    }

    public void Dispose()
    {
        _disposables?.Dispose();
    }
}