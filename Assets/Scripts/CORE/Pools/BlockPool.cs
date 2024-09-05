using Arkanoid;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class BlockPool : Pool<Collider>
{
    public BlockPool(GameObject prefab, Factory factory, IDataProvider<Collider> dataProvider) : base(prefab, factory)
    {
        _pool.AddRange(dataProvider.GetArray());
        
        /*foreach (var block in dataProvider.GetArray())
        {
            block.OnCollisionEnterAsObservable().Subscribe(_ =>
            {
                Buff obj = null;
                int rand = Random.Range(0, 2);
                switch (rand)
                {
                    case 0:
                        obj = _multipleBallPool.Pop();
                        break;
                    case 1:
                        obj = _reduceSizePool.Pop();
                        break;
                }
                
                obj.transform.SetPositionAndRotation(block.transform.position, Quaternion.identity);
                block.gameObject.SetActive(false);
            }).AddTo(_disposables);
        }*/
    }
}
