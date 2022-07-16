using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PushDetection : MonoBehaviour
{
    public GameObject _director;
    public GlobalParameters _globalParameters;

    public PushableObjectHitboxScript _topHitbox;
    public PushableObjectHitboxScript _bottomHitbox;
    public PushableObjectHitboxScript _leftHitbox;
    public PushableObjectHitboxScript _rightHitbox;

    public bool IsBeingPushed { get; set; }

    public float _pushedTime = 0f;

    private void Start()
    {
        _globalParameters = _director.GetComponent<GlobalParameters>();
    }

    public Direction? CanBePushedInThisDirection()
    {
        if (_topHitbox.IsActive) return Direction.Down;
        if (_bottomHitbox.IsActive) return Direction.Up;
        if (_leftHitbox.IsActive) return Direction.Right;
        if (_rightHitbox.IsActive) return Direction.Left;

        return null;
    }

    private void FixedUpdate()
    {
        if (!IsBeingPushed)
        {
            _pushedTime = 0f;
            return;
        }
        
        _pushedTime += Time.deltaTime;
        if (_pushedTime > _globalParameters._requiredPushingTime) Push();
    }

    internal void Push()
    {
        var dir = CanBePushedInThisDirection();
        if (!dir.HasValue)
        {
            Debug.Log($"ERROR: We try to push {name}, but no direction is available");
            return;
        }

        // Code voor het duwen.
    }
}
