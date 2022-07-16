using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal class LayerOrderFixer : MonoBehaviour
{
    private Renderer _renderer;
    public bool _text = false;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        int textMod = 0;
        if (_text) textMod = 10;
        _renderer.sortingOrder = (int)(-transform.position.y * 10+textMod);
    }
}
