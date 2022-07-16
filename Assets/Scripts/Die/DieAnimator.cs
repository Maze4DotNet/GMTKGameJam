using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAnimator : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public List<Sprite> _upAnim;
    public List<Sprite> _downAnim;
    public List<Sprite> _leftAnim;
    public List<Sprite> _rightAnim;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void ChangeSprite(int frame)
    {
        _spriteRenderer.sprite = _upAnim[frame];
    }
}
