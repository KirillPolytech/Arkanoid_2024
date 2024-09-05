using UnityEngine;
using Zenject;
using Arkanoid.Settings;

public class Ball : MonoBehaviour
{
    public Rigidbody Rb { get; private set; }
    
    private Vector3 _lastVelocity;
    private Settings _settings;

    private bool _isActive;
    
    [Inject]
    public void Construct(Settings settings)
    {
        _settings = settings;
        
        Rb = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        _lastVelocity = Rb.velocity;
    }
    
    public void Initialize(Vector3 velocity, Vector3 pos)
    {
        Rb.position = pos;
        Rb.velocity = velocity;
    }

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    private void OnCollisionEnter(Collision other)
    {
        Vector3 inDir = _lastVelocity;
        Vector3 reflect = Vector3.Reflect(inDir, other.contacts[0].normal);

        Vector3 additionalDir = Vector3.zero;

        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

        if (rb)
            additionalDir = rb.velocity;
        
        Rb.velocity = reflect.normalized * _settings.BallStartForce + additionalDir;
    }

    private void OnDrawGizmos()
    {
        if (!Rb)
            return;

        Debug.DrawRay(transform.position, Rb.velocity.normalized, Color.green);
    }
}