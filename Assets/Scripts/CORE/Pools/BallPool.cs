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

    private readonly CompositeDisposable _disposables = new CompositeDisposable();
    private readonly Factory _factory;
    protected readonly List<Ball> _pool = new List<Ball>();
    private readonly Rigidbody _prefab;
    private readonly Settings _settings;

    private event Action FixedUpdate;

    public BallPool(Rigidbody prefab, Factory factory, Settings settings)
    {
        _factory = factory;
        _settings = settings;
        _prefab = prefab;

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
        return freeBall;
    }

    public void Push(GameObject ball)
    {
        Ball firstOrDefault = _pool.FirstOrDefault(x => x.GameObject == ball);
        firstOrDefault?.GameObject.SetActive(false);
    }

    public Ball[] GetActive() => _pool.Where(x => x.GameObject.activeSelf).ToArray();

    public void Reset()
    {
        foreach (var ball in _pool)
        {
            ball?.GameObject.SetActive(false);
        }
    }

    public void Dispose()
    {
        _disposables?.Dispose();

        foreach (var ball in _pool)
        {
            FixedUpdate -= ball.FixedTick;
        }
    }
}