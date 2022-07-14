using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericSpawner : MonoBehaviour
{
    public GameObject _spawnManager;
    public SpawnManagerScript SpawnManagerScript
    {
        get
        {
            return _spawnManager.GetComponent<SpawnManagerScript>();
        }
    }

    [SerializeField, Range(0f, 100f)] private float _spawnTime;
    [SerializeField, Range(0f, 100f)] private float _deSpawnTime;

    public void StartAutoSpawn()
    {
        StartCoroutine(WaitThenSpawn());
    }

    public void StopAutoSpawn()
    {
        StopCoroutine(WaitThenSpawn());
    }

    IEnumerator WaitThenSpawn()
    {
        yield return new WaitForSeconds(_spawnTime);
        SpawnItem();
        StartCoroutine(WaitThenSpawn());
    }

    IEnumerator WaitThenDespawn(GameObject obj)
    {
        yield return new WaitForSeconds(_deSpawnTime);
        Destroy(obj);
    }

    public abstract GameObject SpawnItem();

    public abstract GameObject SpawnItemAtLocation(Vector2 point);
}
