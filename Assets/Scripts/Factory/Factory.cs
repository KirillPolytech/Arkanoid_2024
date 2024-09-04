using UnityEngine;

public class Factory
{
    public T CreateInstance<T>(T prefab) where T : Object
    {
        return Object.Instantiate(prefab);
    }
}
