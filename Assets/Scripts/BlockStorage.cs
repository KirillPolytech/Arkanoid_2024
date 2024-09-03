using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class BlockStorage : MonoBehaviour
{
    [SerializeField] private Collider[] blocks;
    
    private void Awake()
    {
        foreach (var block in blocks)
        {
            block.OnCollisionEnterAsObservable().Subscribe(t => block.gameObject.SetActive(false));
        }
    }
}
