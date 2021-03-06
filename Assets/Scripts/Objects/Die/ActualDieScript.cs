using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualDieScript : MonoBehaviour
{
    [SerializeField, Range(1, 6)] private int _topNumber = 1; 
    [SerializeField, Range(1, 6)] private int _frontNumber = 2;
    [SerializeField] private bool _forceInit = false;

    public GameObject _confettiBoi;
    public GameObject _victoryDie;
    public PushDetection _pushDetection;
    public LevelWin _levelWin;
    public SoundManager _soundManager;

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
    public Direction _lastDir = Direction.Down;
    public bool _won = false;


    public bool BounceBackWhenDone { get; set; } = false;
    public bool Rotating { get; set; } = false;

    private void Update()
    {
        if (_forceInit)
        {
            InitDie(_topNumber, _frontNumber);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _alpha = 1;
        InitDie(1, 2);

        StartCoroutine(WaitThenDissappear());
    }

    private void InitDie(int top, int front)
    {
        _die.Init(top, front);
        SetColor();
        SetNumbers();
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
        if (_won || Rotating) alpha = 0;
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
        _soundManager.PlaySound("red-button");
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
        _pushDetection.ActualRoll(dir, true);
    }

    internal void Win()
    {
        _pushDetection.CanBePushed = false;
        _soundManager.PlaySound("green-button");
        var player = GameObject.FindWithTag("Player");
        var movement = player.GetComponent<PlayerMovement>();
        movement.Victory();

    }

    IEnumerator WaitThenConfetti()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(_victoryDie, new Vector3( transform.position.x,transform.position.y+0.1f,0), Quaternion.identity);
        var spriteRenderer = GetComponentInParent<SpriteRenderer>();
        spriteRenderer.color = new Color(0, 0, 0, 0);
        yield return new WaitForSeconds(0.15f);
        Instantiate(_confettiBoi, new Vector3(transform.position.x, transform.position.y + 0.2f,0),Quaternion.identity);
        _soundManager.PlaySound("confetti");
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

    public void Rotate(int dir)
    {
        _die.Rotate(dir);
        SetNumbers();
        Rotating = false;
        //StartCoroutine(WaitThenPutNumbersBack());
    }

    IEnumerator WaitThenPutNumbersBack()
    {
        yield return new WaitForSeconds(0.1f);
        Rotating = false;
    }
}
