using UnityEngine;

public abstract class Buff
{
    protected readonly GameObject _buffGameObject;

    protected Buff(GameObject buffGameObject)
    {
        _buffGameObject = buffGameObject;
    }
    
    public abstract void Execute();
}