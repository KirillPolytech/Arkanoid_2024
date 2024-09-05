using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

public class BlockStorage : MonoBehaviour
{
    private readonly CompositeDisposable _disposables = new CompositeDisposable();

    [SerializeField] private Collider[] blocks;

    [SerializeField] private Buff buffPrefab;

    private Pool<Buff> _poolBuff;

    [Inject]
    public void Construct(Arkanoid.Factory factory)
    {
        _poolBuff = new Pool<Buff>(buffPrefab.gameObject, factory);
    }

    private void Awake()
    {
        foreach (var block in blocks)
        {
            block.OnCollisionEnterAsObservable().Subscribe(_ =>
            {
                var obj = _poolBuff.Pop();
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