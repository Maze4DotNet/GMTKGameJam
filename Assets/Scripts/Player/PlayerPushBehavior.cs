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
        var dir = DirVec.GetVector(facingDirection);
        dx = (int)dir.x;
        dy = (int)dir.y;
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
        var dir = DirVec.GetVector(pushInDir);

        bool xCorrect = _playerMovement._movement.x == dir.x;
        bool yCorrect = _playerMovement._movement.y == dir.y;

        pushDetection.IsBeingPushed = xCorrect && yCorrect;
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
