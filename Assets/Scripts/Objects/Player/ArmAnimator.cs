using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmAnimator : MonoBehaviour
{
    private Animator _anim;
    private SpriteRenderer _spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _anim = gameObject.GetComponent<Animator>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        UnrenderArms();
    }
    public void RenderArms(Direction dir){
        _spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        switch (dir)
        {
            case Direction.Up:
                _anim.SetBool("up", true);
                break;
            case Direction.Down:
                _anim.SetBool("down", true);
                break;
            case Direction.Left:
                _anim.SetBool("left", true);
                break;
            case Direction.Right:
                _anim.SetBool("right", true);
                break;
        }
    }
    public void UnrenderArms(){
        _spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        _anim.SetBool("up", false);
        _anim.SetBool("down", false);
        _anim.SetBool("left", false);
        _anim.SetBool("right", false);
    }
}
