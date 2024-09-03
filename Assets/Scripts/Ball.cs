using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;
using Arkanoid.Settings;

public class Ball : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector3 _lastVelocity;
    private Settings _settings;

    [Inject]
    public void Construct(Settings settings)
    {
        _settings = settings;
        _rb = GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        float randX = Random.Range(-_settings.StartRange, _settings.StartRange);
        Vector3 randomDir = (Vector3.up + new Vector3(randX, 0, 0)) * _settings.BallStartForce;
        _rb.velocity = randomDir;
    }

    private void FixedUpdate()
    {
        _lastVelocity = _rb.velocity;
    }

    private void OnCollisionEnter(Collision other)
    {
        Vector3 inDir = _lastVelocity;
        Vector3 reflect = Vector3.Reflect(inDir, other.contacts[0].normal);
        
        _rb.velocity = reflect;
    }

    private void OnDrawGizmos()
    {
        if (!_rb)
            return;

        Debug.DrawRay(transform.position, _rb.velocity.normalized, Color.green);
    }
}