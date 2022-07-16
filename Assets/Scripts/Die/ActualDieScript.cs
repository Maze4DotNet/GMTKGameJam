using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualDieScript : MonoBehaviour
{
    public PushDetection _pushDetection;
    private DieDatastructure _die = new DieDatastructure();
    public TextMesh _top;
    public TextMesh _bottom;
    public TextMesh _left;
    public TextMesh _right;
    public TextMesh _back;
    public TextMesh _front;
    [SerializeField, Range(0f, 2f)] private float _knippertime = 0.5f;
    private int _knipper = 1;
    private int _alpha = 1;

    // Start is called before the first frame update
    void Start()
    {
        _die.Init(1, 2);
        _alpha = 1;
        SetColor();
        SetNumbers();

        StartCoroutine(WaitThenDissappear());
    }

    IEnumerator WaitThenDissappear()
    {
        yield return new WaitForSeconds(_knippertime);
        _knipper = 0;
        SetColor();
        StartCoroutine(WaitThenAppear());
    }

    IEnumerator WaitThenAppear()
    {
        yield return new WaitForSeconds(_knippertime);
        _knipper = 1;
        SetColor();
        StartCoroutine(WaitThenDissappear());
    }

    public void SetColor()
    {
        Color mainColor = new Color(0, 0, 0, _alpha);
        Color otherColor = new Color(0,0,0, 0.5f * _alpha * _knipper);

        _top.color = mainColor;
        _front.color = mainColor;

        _bottom.color = otherColor;
        _left.color =   otherColor;
        _right.color =  otherColor;
        _back.color = otherColor;
    }

    internal void Roll(Direction dir)
    {
        _alpha = 0;
        SetColor();
        _die.Roll(dir);
        SetNumbers();
    }

    private void SetNumbers()
    {
        _top.text = _die.Top.ToString();
        _bottom.text = _die.Bottom.ToString();
        _left.text = _die.Left.ToString();
        _right.text = _die.Right.ToString();
        _front.text = _die.Front.ToString();
        _back.text = _die.Back.ToString();
    }

    internal void StopRolling()
    {
        _alpha = 1;
        SetColor();
    }
}
