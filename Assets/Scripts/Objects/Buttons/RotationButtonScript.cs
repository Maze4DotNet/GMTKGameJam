using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationButtonScript : MonoBehaviour
{
    [SerializeField, Range(-1,1)] public int _dir;
    [SerializeField] public bool _reverses = false;

    public void Rotate(ActualDieScript dieScript)
    {
        dieScript.Rotate(_dir);
        if (_reverses) _dir *= -1;
    }
}
