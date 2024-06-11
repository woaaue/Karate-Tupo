using UnityEngine;
using System.Collections.Generic;

public sealed class PoolTree : PoolMono<Tree>
{
    [SerializeField] private Tree _prefab;
    [SerializeField] private int _count;
    [SerializeField] private Transform _parent;

    private void Start() => Initialize(_prefab, _parent, _count);

    public List<Tree> GetPool()
    {
        return _pool;
    }
}
