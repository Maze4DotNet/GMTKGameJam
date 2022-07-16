using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal class PlayerPushBehavior : MonoBehaviour
{
    public PlayerMovement _playerMovement;

    private List<GameObject> _currentPushedObject = new List<GameObject>();

    public ArmAnimator _armAnimator;

    public bool IsPushing { get; set; } = false;

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

        var pushInDir = maybePushInDir.Value;
        var dir = DirVec.GetVector(pushInDir);

        bool xCorrect = _playerMovement._movement.x == dir.x;
        bool yCorrect = _playerMovement._movement.y == dir.y;
        bool correct = xCorrect && yCorrect;

        pushDetection.IsBeingPushed = correct;

        if (correct)
        {
            _armAnimator.RenderArms(pushInDir);
            if (!_currentPushedObject.Contains(other)) _currentPushedObject.Add(other);
        }
        else
        {
            RemovePushableObject(other);
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        var other = collision.gameObject;
        var pushDetection = other.GetComponent<PushDetection>();
        if (pushDetection is null) return;

        RemovePushableObject(other);
    }
    private void RemovePushableObject(GameObject other)
    {
        _currentPushedObject.Remove(other);
        if (_currentPushedObject.Count == 0) _armAnimator.UnrenderArms();
    }
}
