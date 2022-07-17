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
    [SerializeField] private bool _startOpen;
    [SerializeField] private bool _open = false;

    // Start is called before the first frame update
    void Awake()
    {
        
        _body = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sprite = _normal;
        if (_startOpen)
        {
            _open = true;
            Open();
        }
    }

    // Update is called once per frame
    public void Switch()
    {
        if (_open) Close();
        else Open();
    }

    internal void Open()
    {
        _boxCollider.enabled = false;
        _alpha = 0;
        _renderer.sprite = _pressed;

        LayerOrderFixer fixer = GetComponent<LayerOrderFixer>();
        _renderer.sortingOrder = -400;
        fixer.Active = false;
        _open = true;
        //SetColor();
    }

    internal void Close()
    {
        _boxCollider.enabled = true;
        _alpha = 1;
        _renderer.sprite = _normal;

        LayerOrderFixer fixer = GetComponent<LayerOrderFixer>();
        fixer.Active = true;
        _open = false;
        //SetColor();
    }

    internal void SetColor()
    {
        var color = _renderer.color;
        _renderer.color = new Color(color.r, color.g, color.b, _alpha);

    }

    internal void GoToDefaultState()
    {
        if (_startOpen) Open();
        else Close();
    }
}
