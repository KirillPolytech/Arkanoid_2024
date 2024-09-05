using UnityEngine;
using Zenject;
using Arkanoid.Settings;

public class Ball : MonoBehaviour
{
    private const float BlindArea = 0.25f;
    
    public Rigidbody Rb { get; private set; }
    
    private Vector3 _lastVelocity;
    private Settings _settings;
    private bool _initialized;
    
    [Inject]
    public void Construct(Settings settings)
    {
        _settings = settings;
        
        Rb = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        if (!_initialized)
            return;
        
        _lastVelocity = Rb.velocity;

        float absY = Mathf.Abs(Rb.velocity.y); 
        float absX = Mathf.Abs(Rb.velocity.x); 
        if (absY < BlindArea || absX < BlindArea)
            Rb.velocity += new Vector3(0, Random.Range(-1, 2) * 1/BlindArea, 0);
    }
    
    public void Initialize(Vector3 velocity, Vector3 pos)
    {
        Rb.position = pos;
        Rb.velocity = velocity;

        _initialized = true;
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
        
        Rb.velocity = (reflect + additionalDir).normalized * _settings.BallStartForce;
    }

    private void OnDrawGizmos()
    {
        if (!Rb)
            return;

        Debug.DrawRay(transform.position, Rb.velocity.normalized, Color.green);
    }
}