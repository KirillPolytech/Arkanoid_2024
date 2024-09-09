using System;
using System.Linq;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Block
{
    private readonly Pool<Collider> _buff;
    private readonly BallPool _ballPool;

    public event Action<int> OnHit;
    public event Action OnDestruct;

    private int _hitToDestruct;

    public Block(
        BlockData blockData,
        Pool<Collider> buff,
        int hitToDestruct,
        CompositeDisposable disposables,
        BallPool ballPool)
    {
        _buff = buff;
        _hitToDestruct = hitToDestruct;
        _ballPool = ballPool;

        BlockView blockView = new BlockView(blockData.rend, blockData.text);
        
        OnHit += blockView.UpdateText;

        OnHit?.Invoke(hitToDestruct);

        OnEnable(blockData, blockView, disposables);

        OnDestroy(blockData, blockView, disposables);

        OnCollsion(blockData.Col, disposables);
    }

    private void OnEnable(BlockData blockData, BlockView blockView, CompositeDisposable disposables)
    {
        blockData.Col.gameObject.OnEnableAsObservable()
            .Subscribe(_ => OnHit += blockView.UpdateText)
            .AddTo(disposables);
    }

    private void OnDestroy(BlockData blockData, BlockView blockView, CompositeDisposable disposables)
    {
        blockData.Col.gameObject.OnDestroyAsObservable()
            .Subscribe(_ => OnHit -= blockView.UpdateText)
            .AddTo(disposables);
    }

    private void OnCollsion(Collider block, CompositeDisposable disposables)
    {
        block.OnCollisionEnterAsObservable().Subscribe(col =>
        {
            var ball = _ballPool.GetActive().ElementAt(0);

            _hitToDestruct = Mathf.Clamp(_hitToDestruct - ball.Damage, 0 , int.MaxValue);

            OnHit?.Invoke(_hitToDestruct);

            if (_hitToDestruct > 0)
                return;

            HandleCollision(col, block);
        }).AddTo(disposables);
    }

    private void HandleCollision(Collision col, Collider block)
    {
        if (!col.gameObject.CompareTag(TagStorage.BallTag))
            return;

        _buff?.Pop().transform.SetPositionAndRotation(block.transform.position, Quaternion.identity);
        block.gameObject.SetActive(false);

        OnDestruct?.Invoke();
    }
}