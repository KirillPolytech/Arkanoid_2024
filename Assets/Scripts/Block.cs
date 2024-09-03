using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Block : MonoBehaviour
{
    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        
        //_collider.OnCollisionEnterAsObservable().Subscribe(_ => Debug.Log("Collision"));

    }

    // private void OnCollisionEnter(Collision other)
    // {
    //     if (!other.gameObject.CompareTag(TagStorage.BallTag))
    //         return;
    //     
    //     gameObject.SetActive(false);
    // }
}
