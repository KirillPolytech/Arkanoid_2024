using Arkanoid.InputSystem;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;
using Arkanoid.Settings;

public class Ball : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector3 _lastVelocity;
    private Settings _settings;
    private InputHandler _inputHandler;

    private bool _isActive;
    
    [Inject]
    public void Construct(Settings settings, InputHandler inputHandler)
    {
        _settings = settings;
        _inputHandler = inputHandler;

        _inputHandler.OnInputDataUpdate += CheckInput;
        
        _rb = GetComponent<Rigidbody>();
    }

    private void CheckInput(InputData inputData)
    {
        if (inputData.HorizontalInputValue == 0 || _isActive)
            return;
        
        float randX = Random.Range(-_settings.StartRange, _settings.StartRange);
        Vector3 randomDir = (Vector3.up + new Vector3(randX, 0, 0)).normalized * _settings.BallStartForce;
        _rb.velocity = randomDir;

        _isActive = true;
    }

    private void FixedUpdate()
    {
        _lastVelocity = _rb.velocity;
    }

    private void OnCollisionEnter(Collision other)
    {
        Vector3 inDir = _lastVelocity;
        Vector3 reflect = Vector3.Reflect(inDir, other.contacts[0].normal);
        
        _rb.velocity = reflect.normalized * _settings.BallStartForce;
    }

    private void OnDisable()
    {
        _inputHandler.OnInputDataUpdate -= CheckInput;
    }

    private void OnDrawGizmos()
    {
        if (!_rb)
            return;

        Debug.DrawRay(transform.position, _rb.velocity.normalized, Color.green);
    }
}