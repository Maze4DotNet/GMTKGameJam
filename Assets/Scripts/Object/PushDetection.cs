using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PushDetection : MonoBehaviour
{
    public PushableObjectHitboxScript _topHitbox;
    public PushableObjectHitboxScript _bottomHitbox;
    public PushableObjectHitboxScript _leftHitbox;
    public PushableObjectHitboxScript _rightHitbox;

    public Direction? CanBePushedInThisDirection()
    {
        if (_topHitbox.IsActive) return Direction.Down;
        if (_bottomHitbox.IsActive) return Direction.Up;
        if (_leftHitbox.IsActive) return Direction.Right;
        if (_rightHitbox.IsActive) return Direction.Left;

        return null;
    }
}
