using System;
using System.Collections.Generic;
using System.Linq;
using Arkanoid;
using Arkanoid.Settings;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class BallPool : IFixedTickable, IDisposable
{
    protected const int Limit = 100;

    protected readonly List<Ball> _pool = new List<Ball>();
    protected readonly List<Ball> _active = new List<Ball>();
    private readonly CompositeDisposable _disposables = new CompositeDisposable();
    private readonly Factory _factory;
    private readonly Rigidbody _prefab;
    private readonly Settings _settings;
    private readonly HitSoundPlayer _hitSound;

    private event Action FixedUpdate;

    public BallPool(
        Rigidbody prefab, 
        Factory factory, 
        Settings settings, 
        HitSoundPlayer hitSound)
    {
        _factory = factory;
        _settings = settings;
        _prefab = prefab;
        _hitSound = hitSound;
        
        for (int i = 0; i < Limit; i++)
        {
            Instantiate();
        }
    }

    public void FixedTick()
    {
        FixedUpdate?.Invoke();
    }

    private Ball Instantiate()
    {
        if (_pool.Count >= Limit)
            return null;

        GameObject obj = _factory.CreateInstance(_prefab.gameObject);

        Rigidbody rb = obj.GetComponent<Rigidbody>();

        object[] param = { _settings, rb, _disposables };
        Ball ball = _factory.CreateInstance<Ball>(param);

        FixedUpdate += ball.FixedTick;
        ball.OnCollision += _hitSound.Play;

        _pool.Add(ball);

        obj.GameObject().SetActive(false);

        return ball;
    }

    public Ball Pop()
    {
        Ball freeBall = _pool.FirstOrDefault(x => !x.GameObject.activeSelf) ?? Instantiate();

        if (freeBall == null)
            return null;

        freeBall.GameObject.SetActive(true);
        
        _active.Add(freeBall);
        
        return freeBall;
    }

    public void Push(GameObject ball)
    {
        Ball firstOrDefault = _pool.FirstOrDefault(x => x.GameObject == ball);
        firstOrDefault?.GameObject.SetActive(false);
        
        _active.Remove(firstOrDefault);
    }

    public List<Ball> GetActive() => _active;

    public void Reset()
    {
        foreach (var ball in _pool)
        {
            ball?.GameObject.SetActive(false);
        }
    }

    public void Dispose()
    {
        _disposables?.Clear();

        foreach (var ball in _pool)
        {
            FixedUpdate -= ball.FixedTick;
            ball.OnCollision -= _hitSound.Play;
        }
    }
}