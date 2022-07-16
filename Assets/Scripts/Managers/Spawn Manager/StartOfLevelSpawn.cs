using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal class StartOfLevelSpawn : SingleItemSpawner
{
    [SerializeField, Range(-10f,10f)] private float _yOffset = 0f;

    private void Awake()
    {
        var point = new Vector2(gameObject.transform.position.x,gameObject.transform.position.y + _yOffset);
        SpawnItemAtLocation(point);
    }
}
