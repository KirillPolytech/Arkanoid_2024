using System;
using System.Collections;
using Arkanoid.Settings;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public abstract class Buff
{
    protected readonly GameObject _buffGameObject;
    protected readonly Settings _settings;

    protected Buff(GameObject buffGameObject, 
        CompositeDisposable compositeDisposable,
        Settings settings)
    {
        _buffGameObject = buffGameObject;
        _settings = settings;
        
        Action action = () => Moving().ToObservable().Subscribe().AddTo(compositeDisposable);
        _buffGameObject.OnEnableAsObservable().Subscribe(x => action.Invoke()).AddTo(compositeDisposable);
    }
    
    public abstract void Execute();

    protected IEnumerator Moving()
    {
        while (_buffGameObject.activeSelf)
        {
            _buffGameObject.transform.position += Vector3.down * _settings.DropVelocity * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
}