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

    private void OnCollisionStay2D(Collision2D collision)
    {
        var other = collision.gameObject;
        var pushDetection = other.GetComponent<PushDetection>();
        if (pushDetection is null) return;

        var maybePushInDir = pushDetection.CanBePushedInThisDirection();
        if (maybePushInDir is null) return;

        bool stillPushing = false;
        var pushInDir = maybePushInDir.Value;
        if (pushInDir == _playerMovement.CurrentFacingDirection)
        {
            switch (pushInDir)
            {
                case (Direction.Up):
                {
                    if (_playerMovement._movement.y == 1) stillPushing = true;
                    break;
                }
                case (Direction.Down):
                {
                    if (_playerMovement._movement.y == -1) stillPushing = true;
                    break;
                }
                case (Direction.Left):
                {
                    if (_playerMovement._movement.x == -1) stillPushing = true;
                    break;
                }
                default:
                {
                    if (_playerMovement._movement.x == 1) stillPushing = true;
                    break;
                }
            }
        }
        pushDetection.IsBeingPushed = stillPushing;
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

    private void OnCollisionExit2D(Collision2D collision)
    {
        var other = collision.gameObject;
        var pushDetection = other.GetComponent<PushDetection>();
        if (pushDetection is null) return;

        pushDetection.IsBeingPushed = false;
    }
}
