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
    public bool CanBeShot { get; set; } = true;
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

    internal void Shot(int dx, int dy)
    {
        Direction dir = DirVec.GetDirection(new Vector2(dx, dy));
        if (CanBePushed && CanBeShot) ActualRoll(dir, false);
    }

    internal void Push()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().Wait();
        IsBeingPushed = false;
        var maybeDir = CanBePushedInThisDirection();
        if (!maybeDir.HasValue)
        {
            Debug.Log($"ERROR: We try to push {name}, but no direction is available");
            return;
        }
        var dir = maybeDir.Value;
        ActualRoll(dir, true);
    }

    public void ActualRoll(Direction dir, bool rollOrSlide)
    {
        if (!CanBePushed) return;
        CanBeShot = false;
        CanBePushed = false;
        _actualDieScript._lastDir = dir;
        var vec = DirVec.GetVector(dir);

        var nextPos = transform.position + new Vector3(vec.x * _globalParameters._rollSpeed, vec.y * _globalParameters._rollSpeed, 0f);
        var overlap = Physics2D.OverlapBoxAll(nextPos, new Vector2(_globalParameters._rollSpeed / 2f, _globalParameters._rollSpeed / 2f), 0);
        if (!(overlap is null))
        {
            foreach (var obj in overlap)
            {
                if (obj.name.Contains("Wall") || obj.name.Contains("Door") || obj.name.Contains("Lever") || (obj.name.Contains("Die") && obj != gameObject) || _actualDieScript is null) return;
                if (obj.name.Contains("Rotation"))
                {
                    _rotationButtonScript = obj.gameObject.GetComponent<RotationButtonScript>();
                    WillRotateWhenDone = true;
                    _actualDieScript.Rotating = true;
                    break;
                }
                if (obj.name.Contains("Red"))
                {
                    CanBeShot = false;
                }
            }
        }
        if (rollOrSlide)
        {
            _soundManager.PlaySound("die-push");
            _actualDieScript.Roll(dir);
        }
        else
        {
            //_soundManager.PlaySound("bullet-hitting-die");
        }
        PushOn(dir, vec, rollOrSlide);
    }

    internal void PushOn(Direction dir, Vector2 vec, bool rollOrSlide)
    {
        transform.position = transform.position + new Vector3(vec.x * _globalParameters._rollSpeed / 4, vec.y * _globalParameters._rollSpeed / 4, 0f);
        PushPhase = (PushPhase + 1) % 4;
        if (rollOrSlide) _dieAnimator.ChangeSprite(PushPhase, dir);
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
                StartCoroutine(WaitThenRotateFurther(rotationDir, rollOrSlide));
            }
            CanBePushed = true;
            StartCoroutine(WaitThenEnableShooting());
            return;
        }
        StartCoroutine(WaitThenPushOn(dir, vec, rollOrSlide));
    }


    IEnumerator WaitThenPushOn(Direction dir, Vector2 vec, bool rollOrSlide)
    {
        yield return new WaitForSeconds(_globalParameters._rollDuration / 4);
        PushOn(dir, vec, rollOrSlide);
    }

    IEnumerator WaitThenEnableShooting()
    {
        yield return new WaitForSeconds(0.5f);
        CanBeShot = true;
    }

    IEnumerator WaitThenRotateFurther(int dir, bool rollOrSlide)
    {
        yield return new WaitForSeconds(_globalParameters._rollDuration / 4);
        RotatePhase = (RotatePhase + 1) % 4;
        _dieAnimator.SpinSprite(RotatePhase, dir);
        if (RotatePhase == 0) _actualDieScript.Rotate(dir);
        else StartCoroutine(WaitThenRotateFurther(dir, rollOrSlide));
    }
}
