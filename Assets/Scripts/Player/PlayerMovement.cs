using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] public float _moveSpeed = 5f;
    public Rigidbody2D _body;
    Vector2 _movement;

    public Direction CurrentFacingDirection { get; set; } = Direction.Down;

    // Update is called once per frame
    void Update()
    {
        // Input
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");

    }

    private void FixedUpdate()
    {
        // Movement
        _body.MovePosition(_body.position + _movement * _moveSpeed * Time.fixedDeltaTime);
    }
}
