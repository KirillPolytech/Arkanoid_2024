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
    private readonly List<Pool<Buff>> _buffs = new List<Pool<Buff>>();
    private readonly List<Block> _blocks = new List<Block>();
    private readonly List<Action> _cachedActions = new List<Action>();

    public Action<int> OnBlockDestruct;

    private Collider[] _blockColliders;
    private PlayerCamera _playerCamera;
    private Settings _settings;

    [Inject]
    public void Construct(
        BlockProvider blockProvider,
        BuffPoolsProvider buffPoolsProvider,
        PlayerCamera playerCamera,
        Settings settings)
    {
        _playerCamera = playerCamera;
        _settings = settings;
        _blockColliders = blockProvider.GetArray();

        _buffs.AddRange(buffPoolsProvider.GetArray());
    }

    public void Initialize()
    {
        foreach (var blockCol in _blockColliders)
        {
            int hitToDestruct = Random.Range(0, _settings.HitToDestructRange);

            BlockData blockData = new BlockData
            {
                Col = blockCol,
                text = blockCol.gameObject.GetComponentInChildren<TextMeshProUGUI>(),
                Canv = blockCol.gameObject.GetComponentInChildren<Canvas>(),
                rend = blockCol.GetComponentInChildren<Renderer>()
            };

            blockData.Canv.worldCamera = _playerCamera.Cam;

            Block block = new Block(blockData, GetRandomPoolBuff(), hitToDestruct, _disposables);

            _blocks.Add(block);

            Action cached = () =>
                OnBlockDestruct?.Invoke(_blockColliders.Count(x => x.gameObject.activeSelf));

            _cachedActions.Add(cached);

            block.OnDestruct += cached.Invoke;
        }
    }

    private Pool<Buff> GetRandomPoolBuff()
    {
        int rand = Random.Range(0, _settings.DropProbability);
        return rand >= _buffs.Count ? null : _buffs.ElementAt(rand);
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