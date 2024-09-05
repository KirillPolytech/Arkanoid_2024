using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

public class BlockStorage : MonoBehaviour
{
    private readonly CompositeDisposable _disposables = new CompositeDisposable();

    [SerializeField] private Collider[] blocks;

    [SerializeField] private Buff multipleBallPrefab;
    [SerializeField] private Buff reduceSizePrefab;

    private Pool<Buff> _multipleBallPool;
    private Pool<Buff> _reduceSizePool;

    [Inject]
    public void Construct(Arkanoid.Factory factory)
    {
        _multipleBallPool = new Pool<Buff>(multipleBallPrefab.gameObject, factory);
        _reduceSizePool = new Pool<Buff>(reduceSizePrefab.gameObject, factory);
    }

    private void Awake()
    {
        foreach (var block in blocks)
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
        }
    }
    
    private void OnDisable()
    {
        _disposables.Clear();
    }
}