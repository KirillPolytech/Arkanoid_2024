using System;
using System.Collections.Generic;
using Arkanoid;
using UniRx;
using UnityEngine;

public class BuffPoolInstantiator
{
    private const int DefaultAmount = 33;

    private readonly List<Pool<Collider>> _buffs = new List<Pool<Collider>>();

    public BuffPoolInstantiator(
        BuffPrefabProvider buffPrefabProvider,
        Factory factory,
        CompositeDisposable compositeDisposable,
        BallPool ballPool)
    {
        BuffData[] arr = buffPrefabProvider.GetArray();

        for (int i = 0; i < arr.Length; i++)
        {
            List<object> param = new List<object>();
            object[] obj = Array.Empty<object>();

            switch (arr[i].Type)
            {
                case BuffType.DivisionBallsBuff:
                    obj = new object[] { compositeDisposable, ballPool };
                    param.AddRange(obj);

                    BuffPool<DivisionBallsBuff> dBuffPool = new BuffPool<DivisionBallsBuff>(
                        arr[i].Col.gameObject,
                        factory,
                        compositeDisposable,
                        param,
                        DefaultAmount);
                    
                    _buffs.Add(dBuffPool);
                    break;
                case BuffType.ExpandSizeBuff:
                    param = new List<object>();
                    
                    BuffPool<ExpandSizeBuff> eBuffPool = new BuffPool<ExpandSizeBuff>(
                        arr[i].Col.gameObject,
                        factory,
                        compositeDisposable,
                        param,
                        DefaultAmount);
                    
                    _buffs.Add(eBuffPool);
                    break;
                case BuffType.ShrinkSizeBuff:
                    param = new List<object>();
                    
                    BuffPool<ShrinkSizeBuff> sBuffPool = new BuffPool<ShrinkSizeBuff>(
                        arr[i].Col.gameObject,
                        factory,
                        compositeDisposable,
                        param,
                        DefaultAmount);
                    
                    _buffs.Add(sBuffPool);
                    break;
                case BuffType.BallBoostBuff:
                    param = new List<object>();
                    
                    BuffPool<BallDestructionBoostBuff> boostBuff = new BuffPool<BallDestructionBoostBuff>(
                        arr[i].Col.gameObject,
                        factory,
                        compositeDisposable,
                        param,
                        DefaultAmount);
                    
                    _buffs.Add(boostBuff);
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public List<Pool<Collider>> GetPools() => _buffs;
}