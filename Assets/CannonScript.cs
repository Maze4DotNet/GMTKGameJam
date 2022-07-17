using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    public GameObject _bulletPrefab;
    [SerializeField, Range(-1, 1)] private int _dx;
    [SerializeField, Range(-1, 1)] private int _dy;
    [SerializeField, Range(0f, 10f)] private float _reloadTime;

    public SpriteRenderer _renderer;

    public Sprite _right;
    public Sprite _left;
    public Sprite _up;
    public Sprite _down;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();

        if (_dx == 1) _renderer.sprite = _right;
        else if (_dx == -1) _renderer.sprite = _left;
        else if (_dy == 1) _renderer.sprite = _up;
        else _renderer.sprite = _down;

        StartCoroutine(WaitThenShoot());
    }

    IEnumerator WaitThenShoot()
    {
        yield return new WaitForSeconds(_reloadTime);
        Shoot();
    }

    private void Shoot()
    {
        var bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        var bulletScript = bullet.GetComponent<BulletScript>();
        bulletScript.Fly(_dx, _dy);
        StartCoroutine(WaitThenShoot());
    }
}
