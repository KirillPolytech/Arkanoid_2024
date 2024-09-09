using System;
using System.Collections.Generic;
using System.Linq;
using Arkanoid.Settings;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class BlockService : IInitializable, IDisposable
{
    private readonly CompositeDisposable _disposables = new CompositeDisposable();
    private readonly List<Pool<Collider>> _buffs = new List<Pool<Collider>>();
    private readonly List<Block> _blocks = new List<Block>();
    private readonly List<Action> _cachedActions = new List<Action>();

    public Action<int> OnBlockDestruct;

    private Collider[] _blockColliders;
    private Settings _settings;
    private BallPool _ballPool;
    private AudioSource _destructSound;

    [Inject]
    public void Construct(
        BlockProvider blockProvider,
        BuffPoolsProvider buffPoolsProvider,
        Settings settings,
        BallPool ballPool,
        AudioSource destructSound)
    {
        _settings = settings;
        _ballPool = ballPool;
        _destructSound = destructSound;
        _blockColliders = blockProvider.GetArray();

        _buffs.AddRange(buffPoolsProvider.GetArray());
    }

    public void Initialize()
    {
        foreach (var blockCol in _blockColliders)
        {
            int hitToDestruct = Random.Range(1, _settings.HitToDestructRange);

            BlockData blockData = new BlockData
            {
                Col = blockCol,
                text = blockCol.gameObject.GetComponentInChildren<TextMeshProUGUI>(),
                rend = blockCol.GetComponentInChildren<Renderer>()
            };

            Block block = new Block(blockData, 
                GetRandomPoolBuff(), 
                hitToDestruct, 
                _disposables, 
                _ballPool);

            _blocks.Add(block);

            Action cached = () =>
            {
                OnBlockDestruct?.Invoke(_blockColliders.Count(x => x.gameObject.activeSelf));
                _destructSound.Play();
            };

            _cachedActions.Add(cached);

            block.OnDestruct += cached.Invoke;
        }
    }

    private Pool<Collider> GetRandomPoolBuff()
    {
        int rand = Random.Range(0, _settings.DropProbability);
        return rand >= _buffs.Count ? null : _buffs.ElementAt(rand);
    }

    public void Reset()
    {
        for (int i = 0; i < _cachedActions.Count; i++)
        {
            _blocks[i].Reset();
        }
    }

    public void Dispose()
    {
        for (int i = 0; i < _cachedActions.Count; i++)
        {
            _blocks[i].OnDestruct -= _cachedActions[i];
        }

        _cachedActions.Clear();
        _blocks.Clear();

        _disposables?.Dispose();
    }
}