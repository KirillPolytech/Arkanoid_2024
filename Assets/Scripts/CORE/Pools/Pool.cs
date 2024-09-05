using System.Collections.Generic;
using System.Linq;
using Arkanoid;
using Unity.VisualScripting;
using UnityEngine;

public class Pool<T> where T : Component
{
    private const int DefaultAmount = 10;

    private readonly Factory _factory;
    protected readonly List<T> _pool = new List<T>();

    private readonly GameObject _prefab;

    public Pool(GameObject prefab, Factory factory)
    {
        _factory = factory;

        _prefab = prefab;

        for (int i = 0; i < DefaultAmount; i++)
        {
            Instantiate();
        }
    }

    private T Instantiate()
    {
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

        freeGameObject.GameObject().SetActive(true);
        return freeGameObject;
    }

    public void Push(GameObject obj)
    {
        obj.GameObject().SetActive(false);
    }
    
    public T[] GetActive()
    {
        return _pool.Where(x => x.GameObject().activeSelf).ToArray();
    }

    public void Reset()
    {
        foreach (var t in _pool)
        {
            t.GameObject().SetActive(false);
        }
    }
}