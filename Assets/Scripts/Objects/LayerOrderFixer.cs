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
    public bool _groundObject = false;
    public bool Active { get; set; } = true;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (!Active) return;

        int textMod = 0;
        if (_text) textMod = 10;
        int andereMod = 0;
        if (_groundObject) andereMod -= 500;
        _renderer.sortingOrder = (int)(-transform.position.y * 10+textMod + andereMod);
    }
}
