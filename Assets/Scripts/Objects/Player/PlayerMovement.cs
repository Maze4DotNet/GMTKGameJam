using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] public float _moveSpeed = 5f;
    public Rigidbody2D _body;
    public Animator _animator;
    [SerializeField] public Vector2 _movement = new Vector2(0, 0);
    [SerializeField, Range(0, 10)] private int _dieSteps;
    private int _dieStepsAlreadyTaken = 0;
    public bool _isDying = false;
    public bool Blocked { get; set; } = false;

    public Direction CurrentFacingDirection { get; set; } = Direction.Down;

    public void GuyDie()
    {
        _isDying = true;
        DieStep();
        StartCoroutine(WaitThenKeepDying());
    }

    IEnumerator WaitThenKeepDying()
    {
        yield return new WaitForSeconds(0.1f);
        DieStep();
    }

    internal void DieStep()
    {
        var currentScale = transform.localScale;
        transform.localScale = new Vector3(currentScale.x, currentScale.y * (_dieSteps - ++_dieStepsAlreadyTaken)/_dieSteps, currentScale.z);
        if (_dieStepsAlreadyTaken < _dieSteps) StartCoroutine(WaitThenKeepDying());
        else StartCoroutine(WaitThenRestart());
    }

    IEnumerator WaitThenRestart()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        // Input
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");

        if (_movement.x > 0) CurrentFacingDirection = Direction.Right;
        else if (_movement.x < 0) CurrentFacingDirection = Direction.Left;
        else if (_movement.y > 0) CurrentFacingDirection = Direction.Up;
        else if (_movement.y < 0) CurrentFacingDirection = Direction.Down;


        _animator.SetBool("up", false);
        _animator.SetBool("down", false);
        _animator.SetBool("left", false);
        _animator.SetBool("right", false);

        switch (CurrentFacingDirection)
        {
            case Direction.Up:
                _animator.SetBool("up", true);
                break;
            case Direction.Down:
                _animator.SetBool("down", true);
                break;
            case Direction.Left:
                _animator.SetBool("left", true);
                break;
            case Direction.Right:
                _animator.SetBool("right", true);
                break;
        }
    }

    internal void Wait()
    {
        Blocked = true;
        StartCoroutine(WaitThenUnblock());
    }

    IEnumerator WaitThenUnblock()
    {
        yield return new WaitForSeconds(0.15f);
        Blocked = false;
    }

    public void Victory()
    {
        StopAllCoroutines();
        Blocked = true;
        _animator.SetBool("win", true);
    }

    private void FixedUpdate()
    {
        if (_isDying || Blocked) return;
        // Movement
        var nextPos = _body.position + _movement * _moveSpeed * Time.fixedDeltaTime;
        _body.MovePosition(nextPos);
    }
}
