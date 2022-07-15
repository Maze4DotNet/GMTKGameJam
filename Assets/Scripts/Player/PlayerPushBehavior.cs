using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal class PlayerPushBehavior : MonoBehaviour
{
    public PlayerMovement _playerMovement;

    private GameObject _currentPushedObject = null;

    public Direction _pushingDirection { get; private set; }

    public void GetDxDy(out int dx, out int dy)
    {
        var facingDirection = _playerMovement.CurrentFacingDirection;
        switch (facingDirection)
        {
            case Direction.Left:
            {
                dx = -1;
                dy = 0;
                break;
            }
            case Direction.Right:
            {
                dx = 1;
                dy = 0;
                break;
            }
            case Direction.Up:
            {
                dx = 0;
                dy = 1;
                break;
            }
            default:
            {
                dx = 0;
                dy = -1;
                break;
            }
        }
    }

    private bool IsThisThePushedObjectOrNot(GameObject other)
    {
        if (_currentPushedObject is null)
        {
            GetDxDy(out int dx, out int dy);
            var xDist = Math.Sign(other.transform.position.x - transform.position.x);
            var yDist = Math.Sign(other.transform.position.y - transform.position.y);

            if (dx == xDist || dy == yDist) return true;
            else return false;
        }

        bool alreadyPushing = _currentPushedObject == other;
        return alreadyPushing;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var other = collision.gameObject;
        bool pushed = IsThisThePushedObjectOrNot(other);

        if (!pushed) return;
        _currentPushedObject = other;
        _pushingDirection = _playerMovement.CurrentFacingDirection;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        var other = collision.gameObject;
        bool pushed = IsThisThePushedObjectOrNot(other);

        if (pushed) _currentPushedObject = null;
    }
}
