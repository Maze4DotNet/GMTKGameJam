using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField, Range(-1, 1)] public int _dx;
    [SerializeField, Range(-1, 1)] public int _dy;
    [SerializeField, Range(0, 100f)] public float _speed;
    private bool _isFlying = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_isFlying) return;

        var pos = transform.position;
        var newPow = new Vector2(pos.x + _dx * _speed * Time.deltaTime, pos.y + _dy * _speed * Time.deltaTime);
        transform.position = newPow;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var other = collision.gameObject;
        if (other.name.Contains("Die"))
        {
            var pushDetection = other.GetComponent<PushDetection>();
            pushDetection.Shot(_dx, _dy);
        }
        if (other.tag == "Player")
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            player.GuyDie();
        }
        if (other.name.Contains("Wall") || other.name.Contains("Die") || other.name.Contains("Door"))
        {
            Destroy(gameObject);
        }
    }

    internal void Fly(int dx, int dy)
    {
        _dx = dx;
        _dy = dy;
        _isFlying = true;
    }
}
 