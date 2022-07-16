using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Tilemap _map;
    [SerializeField] private List<TileDataScript> _tileData;

    private Dictionary<TileBase, TileDataScript> _tileDict;

    private void Awake()
    {
        _tileDict = new Dictionary<TileBase, TileDataScript>();

        foreach (var tileDataBoi in _tileData)
        {
            foreach (var tile in tileDataBoi._tiles)
            {
                _tileDict.Add(tile, tileDataBoi);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPos = _map.WorldToCell(mousePos);

            TileBase clickedTile = _map.GetTile(gridPos);

            print($"At pos {gridPos} there is a {clickedTile}");
        }
    }
}
