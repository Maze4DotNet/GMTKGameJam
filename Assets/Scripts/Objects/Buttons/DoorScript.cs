using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Rigidbody2D _body;
    public BoxCollider2D _boxCollider;
    public SpriteRenderer _renderer;

    public Sprite _normal;
    public Sprite _pressed;

    private int _alpha = 1;

    // Start is called before the first frame update
    void Start()
    {
        
        _body = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sprite = _normal;
            }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void Open()
    {
        _boxCollider.enabled = false;
        _alpha = 0;
        _renderer.sprite = _pressed;
        _renderer.sortingOrder = -1000;
        var layerOrderFixer = GetComponent<LayerOrderFixer>();
        layerOrderFixer.Active = false;
        //SetColor();
    }

    internal void Close()
    {
        _boxCollider.enabled = true;
        _alpha = 1;
        _renderer.sprite = _normal;
        var layerOrderFixer = GetComponent<LayerOrderFixer>();
        layerOrderFixer.Active = true;

        //SetColor();
    }

    internal void SetColor()
    {
        var color = _renderer.color;
        _renderer.color = new Color(color.r, color.g, color.b, _alpha);

    }
}
