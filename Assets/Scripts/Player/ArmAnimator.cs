using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmAnimator : MonoBehaviour
{
    public bool _renderArms;
    private Animator _parentAnim;
    private Animator _ownAnim;
    private SpriteRenderer _spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _parentAnim = transform.parent.GetComponent<Animator>();
        _ownAnim = gameObject.GetComponent<Animator>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_renderArms)
        {
            _spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            _ownAnim.SetBool("up", _parentAnim.GetBool("up"));
            _ownAnim.SetBool("down", _parentAnim.GetBool("down"));
            _ownAnim.SetBool("left", _parentAnim.GetBool("left"));
            _ownAnim.SetBool("right", _parentAnim.GetBool("right"));
        }
        else
        {
            _spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        }
    }
}
