using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverBehavior : MonoBehaviour
{
    public bool _flipped;
    public Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        if (_flipped) Flip();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var other = collision.gameObject;
        if (other.tag == "Player") FlipOrUnflip();
    }

    private void FlipOrUnflip()
    {
        if (_flipped) UnFlip();
        else Flip();
    }

    public void Flip()
    {
        _flipped = true;
        _animator.SetBool("flipped", true);
        var doorScript = GetComponentInChildren<DoorScript>();
        doorScript.Open();
    }
    public void UnFlip()
    {
        _flipped = false;
        _animator.SetBool("flipped", false);
        var doorScript = GetComponentInChildren<DoorScript>();
        doorScript.Close();
    }
}
