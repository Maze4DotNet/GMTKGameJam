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

    public PushableObjectHitboxScript _topHitbox;
    public PushableObjectHitboxScript _bottomHitbox;
    public PushableObjectHitboxScript _leftHitbox;
    public PushableObjectHitboxScript _rightHitbox;

    public bool IsBeingPushed { get; set; }

    public bool CanBePushed { get { return PushPhase == 0; }  }

    public int PushPhase { get; set; } = 0;

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

        // Code voor het duwen.
        PushOn(dir);
    }
    
    internal void PushOn(Direction dir)
    {
        var vec = DirVec.GetVector(dir);
        
        transform.position = transform.position + new Vector3(vec.x * _globalParameters._rollSpeed / 4, vec.y * _globalParameters._rollSpeed / 4, 0f);
        print(PushPhase);
        PushPhase = (PushPhase + 1) % 4;
        _dieAnimator.ChangeSprite(PushPhase);
        if (PushPhase == 0) return;
        StartCoroutine(WaitThenPushOn(dir));
    }


    IEnumerator WaitThenPushOn(Direction dir)
    {
        yield return new WaitForSeconds(_globalParameters._rollDuration / 4);
        PushOn(dir);
    }
}
