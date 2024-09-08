using System;
using System.Collections.Generic;
using Arkanoid;
using UniRx;
using UniRx.Triggers;
using Unity.VisualScripting;
using UnityEngine;

public class BuffPool<T> : Pool<Collider> where T : Buff
{
    private readonly CompositeDisposable _compositeDisposable;
    protected readonly List<T> _buffPool = new List<T>();

    public BuffPool(
        GameObject prefab,
        Factory factory,
        CompositeDisposable compositeDisposable,
        List<object> param, 
        int amount = DefaultAmount) : base(prefab, factory)
    {
        _compositeDisposable = compositeDisposable;
        
        for (int i = 0; i < amount; i++)
        {
            Instantiate(param);
        }
    }

    protected T Instantiate(List<object> param)
    {
        if (_pool.Count >= Limit)
            return null;

        GameObject obj = _factory.CreateInstance(_prefab);

        obj.name = $"{typeof(T)} {_buffPool.Count}";

        Collider col = obj.GetComponent<Collider>();
        
        object[] parameters = new object[param.Count + 1];
        param.CopyTo(parameters);
        parameters[^1] = col;
        
        T t = _factory.CreateInstance<T>(parameters);
        _buffPool.Add(t);

        Action<Collision> action = collision => OnCollision(collision, t);
        
        col.OnCollisionEnterAsObservable().
            Subscribe(action.Invoke).
            AddTo(_compositeDisposable);
        
        _pool.Add(obj.GetComponent<Collider>());

        obj.GameObject().SetActive(false);

        return t;
        
        void OnCollision(Collision col, T t)
        {
            if (!col.gameObject.CompareTag(TagStorage.PlatformTag))
                return;
        
            t.Execute();
        }
    }
}