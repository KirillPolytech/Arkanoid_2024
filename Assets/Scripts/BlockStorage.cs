using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class BlockStorage : MonoBehaviour
{
    [SerializeField] private Collider[] blocks;

    [SerializeField] private GameObject blockPrefab;

    private Pool<GameObject> _poolBuff;

    private void Awake()
    {
        _poolBuff = new Pool<GameObject>(blockPrefab);

        foreach (var block in blocks)
        {
            block.OnCollisionEnterAsObservable().Subscribe(_ =>
            {
                block.gameObject.SetActive(false);
            });
        }
    }
}