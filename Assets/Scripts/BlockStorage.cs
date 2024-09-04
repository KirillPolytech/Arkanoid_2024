using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class BlockStorage : MonoBehaviour
{
    private readonly CompositeDisposable _disposables = new CompositeDisposable();

    [SerializeField] private Collider[] blocks;

    [SerializeField] private GameObject buffPrefab;

    private Pool<GameObject> _poolBuff;

    private void Awake()
    {
        _poolBuff = new Pool<GameObject>(buffPrefab);

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