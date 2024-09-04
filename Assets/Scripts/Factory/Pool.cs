using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Pool<T> where T : Object
{
    private const int DefaultAmount = 10;

    private readonly Factory _factory = new Factory();
    private readonly List<T> _pool = new List<T>();

    private T _prefab;

    public Pool(T prefab)
    {
        _prefab = prefab;
        
        for (int i = 0; i < DefaultAmount; i++)
        {
            Instantiate();
        }
    }

    private T Instantiate()
    {
        T t = _factory.CreateInstance(_prefab);
        _pool.Add(t);
        
        t.GameObject().SetActive(false);

        return t;
    }

    public T Pop()
    {
        T firstOrDefault = _pool.FirstOrDefault(x => !x.GameObject().activeSelf);

        if (firstOrDefault == null)
            firstOrDefault = Instantiate();

        firstOrDefault.GameObject().SetActive(true);
        return firstOrDefault;
    }

    public void Push(T obj)
    {
        obj.GameObject().SetActive(false);
    }
}