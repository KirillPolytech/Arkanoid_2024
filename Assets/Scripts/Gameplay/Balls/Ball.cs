using UnityEngine;
using Zenject;
using Arkanoid.Settings;
using UniRx;
using UniRx.Triggers;
using Random = UnityEngine.Random;

public class Ball
{
    private const float BlindArea = 0.5f;

    private readonly Settings _settings;
    private Collider _collider;

    public Rigidbody Rb { get; private set; }
    public GameObject GameObject { get; private set; }
    public Transform Transform { get; private set; }

    private Vector3 _lastVelocity;
    private bool _initialized;

    [Inject]
    public Ball(Settings settings, Rigidbody rb, CompositeDisposable disposables)
    {
        _settings = settings;
        Rb = rb;
        Transform = rb.transform;
        GameObject = rb.gameObject;
        _collider = rb.GetComponent<Collider>();


        _collider.OnCollisionEnterAsObservable().Subscribe(OnCollisionEnter).AddTo(disposables);

        _collider.OnDisableAsObservable().Subscribe(_ => OnDisable()).AddTo(disposables);
    }
    
    public void FixedTick()
    {
        if (!_initialized)
            return;

        _lastVelocity = Rb.velocity;
    }

    public void Initialize(Vector3 velocity, Vector3 pos)
    {
        Rb.position = pos;
        Rb.velocity = velocity;

        _initialized = true;
    }

    public void SetPosition(Vector3 pos)
    {
        Transform.position = pos;
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

        CheckBlindDirection();
    }

    private void CheckBlindDirection()
    {
        float absY = Mathf.Abs(Rb.velocity.y);
        float absX = Mathf.Abs(Rb.velocity.x);
        if (absY < BlindArea || absX < BlindArea)
        {
            Rb.velocity += new Vector3(0, Random.Range(-1, 2) * 1 / BlindArea, 0);
        }

        if (absX < BlindArea)
        {
            Rb.velocity += new Vector3(Random.Range(-1, 2) * 1 / BlindArea, 0, 0);
        }
    }

    private void OnDisable()
    {
        Rb.position = Vector3.zero;
        Rb.velocity = Vector3.zero;
        _initialized = false;
    }
}