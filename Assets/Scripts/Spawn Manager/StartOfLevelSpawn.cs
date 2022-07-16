using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal class StartOfLevelSpawn : SingleItemSpawner
{
    private void Awake()
    {
        SpawnItemAtLocation(gameObject.transform.position);
    }
}
