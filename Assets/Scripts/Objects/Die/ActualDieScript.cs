using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualDieScript : MonoBehaviour
{
    public GameObject _confettiBoi;
    public GameObject _victoryDie;
    public PushDetection _pushDetection;
    public LevelWin _levelWin;
    public DieDatastructure _die;
    public TextMesh _top;
    public TextMesh _bottom;
    public TextMesh _left;
    public TextMesh _right;
    public TextMesh _back;
    public TextMesh _front;
    [SerializeField, Range(0f, 2f)] private float _knippertime = 0.5f;
    private int _knipper = 1;
    public int _alpha = 1;
    private Direction _lastDir = Direction.Down;
    public bool _won = false;

    public bool BounceBackWhenDone { get; set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        _die = new DieDatastructure();
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
        int alpha = _alpha;
        if (_won) alpha = 0;
        Color mainColor = new Color(0, 0, 0, alpha);
        Color otherColor = new Color(1,0.5f,0.5f, alpha * _knipper);

        _top.color = mainColor;
        _front.color = mainColor;

        _bottom.color = otherColor;
        _left.color =   otherColor;
        _right.color =  otherColor;
        _back.color = otherColor;
    }

    internal void Roll(Direction dir)
    {
        _lastDir = dir;
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

    internal void BounceBack()
    {
        BounceBackWhenDone = false;
        Direction dir;
        if (_lastDir == Direction.Left) dir = Direction.Right;
        else if (_lastDir == Direction.Right) dir = Direction.Left;
        else if (_lastDir == Direction.Up) dir = Direction.Down;
        else dir = Direction.Up;
        _pushDetection.CanBePushed = false;
        StartCoroutine(WaitThenBounceBack(dir));
    }

    IEnumerator WaitThenBounceBack(Direction dir)
    {
        yield return new WaitForSeconds(0.15f);
        _pushDetection.CanBePushed = true;
        _pushDetection.ActualRoll(dir);
    }

    internal void Win()
    {
        _pushDetection.CanBePushed = false;
    }

    IEnumerator WaitThenConfetti()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(_victoryDie, new Vector3( transform.position.x,transform.position.y+0.1f,0), Quaternion.identity);
        var spriteRenderer = GetComponentInParent<SpriteRenderer>();
        spriteRenderer.color = new Color(0, 0, 0, 0);
        yield return new WaitForSeconds(0.15f);
        Instantiate(_confettiBoi, new Vector3(transform.position.x, transform.position.y + 0.2f,0),Quaternion.identity);
        yield return new WaitForSeconds(2f);
        _levelWin.Win();
    }

    internal void StopNumbers()
    {
        _won = true;
        _alpha = 0;
        StopAllCoroutines();
        SetColor();
        StartCoroutine(WaitThenConfetti());
    }
}
