using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverBehavior : MonoBehaviour
{
    public bool _flipped;
    public Animator _animator;

    public void flip()
    {
        _flipped = true;
        _animator.SetBool("flipped") = true;
    }
    public void unflip()
    {
        _flipped = false;
        _animator.SetBool("flipped") = false;
    }
}
