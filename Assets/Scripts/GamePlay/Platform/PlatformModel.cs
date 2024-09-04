using UnityEngine;
using Zenject;

public class PlatformModel
{
    private readonly Rigidbody _rb;

    [Inject]
    public PlatformModel(Rigidbody rigidbody)
    {
        _rb = rigidbody;
    }

    public void AddForce(float force)
    {
        if (force == 0)
            _rb.velocity = Vector3.zero;
        
        _rb.velocity = new Vector3(force, 0, 0);
    }
}