using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public abstract class SingleItemSpawner : GenericSpawner
{
    GameObject _spawnableItem;

    public List<Vector2> _spawnPoints;
    private Random _random;

    private void Start()
    {
        _random = new Random();
    }

    public override GameObject SpawnItem()
    {
        if (_spawnPoints is null || _spawnPoints.Count == 0) return null;
        var index = _random.Next(_spawnPoints.Count);
        var point = _spawnPoints[index];
        return SpawnItemAtLocation(point);
    }

    public override GameObject SpawnItemAtLocation(Vector2 point)
    {
        return Instantiate(_spawnableItem);
    }
}
