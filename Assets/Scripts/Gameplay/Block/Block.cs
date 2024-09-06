using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Block
{
    private readonly Pool<Buff> _buff;
    private readonly BlockView _blockView;
    
    public event Action<int> OnHit;
    public event Action OnDestruct;

    private int _hitToDestruct;
    
    public Block(
        BlockData blockData, 
        Pool<Buff> buff, 
        int hitToDestruct, 
        CompositeDisposable disposables)
    {
        _buff = buff;
        _hitToDestruct = hitToDestruct;

        _blockView = new BlockView(blockData.text);
        
        OnHit += _blockView.UpdateText;

        OnHit?.Invoke(hitToDestruct);

        blockData.Col.gameObject.OnEnableAsObservable().
            Subscribe(_ => OnHit += _blockView.UpdateText).AddTo(disposables);
        blockData.Col.gameObject.OnDestroyAsObservable().
            Subscribe(_ => OnHit -= _blockView.UpdateText).AddTo(disposables);
        
        SubsribeToCollsion(blockData.Col, disposables);
    }

    private void SubsribeToCollsion(Collider block, CompositeDisposable disposables)
    {
        block.OnCollisionEnterAsObservable().Subscribe(col =>
        {
            _hitToDestruct--;
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
