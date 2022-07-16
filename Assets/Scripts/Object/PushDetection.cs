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
    public GameObject _director;
    private GlobalParameters _globalParameters;
    public DieAnimator _dieAnimator;
    public ActualDieScript _actualDieScript;

    public PushableObjectHitboxScript _topHitbox;
    public PushableObjectHitboxScript _bottomHitbox;
    public PushableObjectHitboxScript _leftHitbox;
    public PushableObjectHitboxScript _rightHitbox;

    public bool IsBeingPushed { get; set; }
    public bool CanBePushed { get { return PushPhase == 0; }  }
    public int PushPhase { get; set; } = 0;

    public Vector2 _originalPos;

    public float _pushedTime = 0f;

    private void Start()
    {
        _director = GameObject.Find("Director");
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
        if (!IsBeingPushed || !CanBePushed)
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
        var vec = DirVec.GetVector(dir);

        if (!(_actualDieScript is null)) _actualDieScript.Roll(dir);

        var nextPos = transform.position + new Vector3(vec.x * _globalParameters._rollSpeed, vec.y * _globalParameters._rollSpeed, 0f);

        var overlap = Physics2D.OverlapBox(nextPos, new Vector2(_globalParameters._rollSpeed / 2f, _globalParameters._rollSpeed / 2f), 0);
        if (!(overlap is null) && overlap.name.Contains("Wall")) return;
        // Code voor het duwen.
        PushOn(dir, vec);
    }
    
    internal void PushOn(Direction dir, Vector2 vec)
    {
        
        transform.position = transform.position + new Vector3(vec.x * _globalParameters._rollSpeed / 4, vec.y * _globalParameters._rollSpeed / 4, 0f);
        print(PushPhase);
        PushPhase = (PushPhase + 1) % 4;
        _dieAnimator.ChangeSprite(PushPhase);
        if (PushPhase == 0)
        {
            if (!(_actualDieScript is null)) _actualDieScript.StopRolling();
            return;
        }
        StartCoroutine(WaitThenPushOn(dir, vec));
    }


    IEnumerator WaitThenPushOn(Direction dir, Vector2 vec)
    {
        yield return new WaitForSeconds(_globalParameters._rollDuration / 4);
        PushOn(dir, vec);
    }
}
