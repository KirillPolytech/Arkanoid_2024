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

    public void AddForce(float force, bool canMove)
    {
        if (!canMove)
            return;
        
        _rb.velocity = new Vector3(force, 0, 0);
    }
}