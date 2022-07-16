using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationButtonScript : MonoBehaviour
{
    [SerializeField, Range(-1,1)] public int _dir;
    [SerializeField] public bool _reverses = false;
    private SpriteRenderer _renderer;

    public int Press(ActualDieScript dieScript)
    {
        _renderer.color = new Color(0.5f, 0.5f, 0.5f, 1);
        int dir = _dir;
        if (_reverses) _dir *= -1;
        return dir;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var other = collision.gameObject;
        if (other.name.Contains("Die")) UnPress();
    }

    public void UnPress()
    {
        _renderer.color = new Color(1,1,1, 1);

    }
}
