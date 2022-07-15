using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] public float _moveSpeed = 5f;
    public Rigidbody2D _body;
    public Animator _animator;
    public Vector2 _movement = new Vector2(0, 0);

    public Direction CurrentFacingDirection { get; set; } = Direction.Down;

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

    private void FixedUpdate()
    {
        // Movement
        _body.MovePosition(_body.position + _movement * _moveSpeed * Time.fixedDeltaTime);
    }
}
