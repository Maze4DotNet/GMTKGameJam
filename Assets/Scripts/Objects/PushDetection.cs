using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PushDetection : MonoBehaviour
{
    public GlobalParameters _globalParameters;
    public DieAnimator _dieAnimator;
    public ActualDieScript _actualDieScript;
    public SoundManager _soundManager;

    public PushableObjectHitboxScript _topHitbox;
    public PushableObjectHitboxScript _bottomHitbox;
    public PushableObjectHitboxScript _leftHitbox;
    public PushableObjectHitboxScript _rightHitbox;

    public bool IsBeingPushed { get; set; }
    public bool PhaseIsZero { get { return PushPhase == 0; } }

    public bool CanBePushed { get; set; } = true;
    public int PushPhase { get; set; } = 0;
    public bool WillRotateWhenDone { get; set; }
    public int RotatePhase { get; set; } = 0;
    private RotationButtonScript _rotationButtonScript;

    public Vector2 _originalPos;

    public float _pushedTime = 0f;

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
        if (!IsBeingPushed || !CanBePushed || !PhaseIsZero)
        {
            _pushedTime = 0f;
            return;
        }

        _pushedTime += Time.deltaTime;
        if (_pushedTime > _globalParameters._requiredPushingTime) Push();
    }

    internal void Push()
    {
        IsBeingPushed = false;
        var maybeDir = CanBePushedInThisDirection();
        if (!maybeDir.HasValue)
        {
            Debug.Log($"ERROR: We try to push {name}, but no direction is available");
            return;
        }
        var dir = maybeDir.Value;
        ActualRoll(dir);
    }

    public void ActualRoll(Direction dir)
    {
        var vec = DirVec.GetVector(dir);

        var nextPos = transform.position + new Vector3(vec.x * _globalParameters._rollSpeed, vec.y * _globalParameters._rollSpeed, 0f);
        var overlap = Physics2D.OverlapBox(nextPos, new Vector2(_globalParameters._rollSpeed / 2f, _globalParameters._rollSpeed / 2f), 0);
        if (!(overlap is null))
        {
            if (overlap.name.Contains("Wall") || _actualDieScript is null) return;
            if (overlap.name.Contains("Rotation"))
            {
                _rotationButtonScript = overlap.gameObject.GetComponent<RotationButtonScript>();
                WillRotateWhenDone = true;
                _actualDieScript.Rotating = true;
            }
        }
        _soundManager.PlaySound("die-push");
        _actualDieScript.Roll(dir);
        PushOn(dir, vec);
    }

    internal void PushOn(Direction dir, Vector2 vec)
    {

        transform.position = transform.position + new Vector3(vec.x * _globalParameters._rollSpeed / 4, vec.y * _globalParameters._rollSpeed / 4, 0f);
        PushPhase = (PushPhase + 1) % 4;
        _dieAnimator.ChangeSprite(PushPhase, dir);
        if (PushPhase == 0)
        {
            if (!(_actualDieScript is null)) _actualDieScript.StopRolling();
            if (_actualDieScript.BounceBackWhenDone) _actualDieScript.BounceBack();
            if (WillRotateWhenDone && !(_rotationButtonScript is null))
            {
                WillRotateWhenDone = false;
                int rotationDir = _rotationButtonScript.Press();
                _actualDieScript.Rotating = true;
                _soundManager.PlaySound("spin");
                StartCoroutine(WaitThenRotateFurther(rotationDir));
            }
            return;
        }
        StartCoroutine(WaitThenPushOn(dir, vec));
    }


    IEnumerator WaitThenPushOn(Direction dir, Vector2 vec)
    {
        yield return new WaitForSeconds(_globalParameters._rollDuration / 4);
        PushOn(dir, vec);
    }

    IEnumerator WaitThenRotateFurther(int dir)
    {
        yield return new WaitForSeconds(_globalParameters._rollDuration / 4);
        RotatePhase = (RotatePhase + 1) % 4;
        _dieAnimator.SpinSprite(RotatePhase, dir);
        if (RotatePhase == 0) _actualDieScript.Rotate(dir);
        else StartCoroutine(WaitThenRotateFurther(dir));
    }
}
