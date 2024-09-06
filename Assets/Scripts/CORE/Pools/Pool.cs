using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Arkanoid;
using Unity.VisualScripting;
using UnityEngine;

public class Pool<T> where T : Component
{
    protected const int DefaultAmount = 10;
    protected const int Limit = 100;

    private readonly Factory _factory;
    protected readonly List<T> _pool = new List<T>();

    private readonly GameObject _prefab;

    public Pool(GameObject prefab, Factory factory, [DefaultParameterValue(DefaultAmount)][Optional] int amount)
    {
        _factory = factory;

        _prefab = prefab;

        for (int i = 0; i < amount; i++)
        {
            Instantiate();
        }
    }

    private T Instantiate()
    {
        if (_pool.Count >= Limit)
            return null;
        
        GameObject obj = _factory.CreateInstance(_prefab);

        T t = obj.GetComponent<T>();
        
        _pool.Add(obj.GetComponent<T>());

        obj.GameObject().SetActive(false);

        return t;
    }

    public T Pop()
    {
        T freeGameObject = _pool.FirstOrDefault(x => !x.GameObject().activeSelf);

        if (freeGameObject == null)
            freeGameObject = Instantiate();

        if (freeGameObject == null)
            return null;

        freeGameObject.GameObject().SetActive(true);
        return freeGameObject;
    }

    public void Push(GameObject obj)
    {
        obj.GameObject().SetActive(false);
    }

    public T[] GetActive() => _pool.Where(x => x.GameObject().activeSelf).ToArray();

    public void Reset()
    {
        foreach (var t in _pool)
        {
            t.GameObject().SetActive(false);
        }
    }
}