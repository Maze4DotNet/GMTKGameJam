using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
internal class TileDataScript : ScriptableObject
{
    public TileBase[] _tiles;

    public bool _movable;
    public bool _pushable;
    public bool _playerSpawn;
}
