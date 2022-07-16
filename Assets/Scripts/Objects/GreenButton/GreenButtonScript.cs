using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GreenButtonScript : MonoBehaviour
{
    public TextMesh _acceptingText;
    public TextMesh _numbersText;
    public List<int> _acceptingNumbers;
    public SpriteRenderer _spriteRenderer;
    [SerializeField, Range(0f, 2f)] private float _knippertime = 0.5f;
    private int _knipper = 1;
    private int _alpha = 1;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        string acceptingString = GetAcceptingString();
        _numbersText.text = $"Accepting:\n{acceptingString}";
        StartCoroutine(WaitThenDissappear());
    }

    IEnumerator WaitThenDissappear()
    {
        yield return new WaitForSeconds(_knippertime);
        _knipper = 0;
        SetColor();
        StartCoroutine(WaitThenAppear());
    }

    IEnumerator WaitThenAppear()
    {
        yield return new WaitForSeconds(_knippertime);
        _knipper = 1;
        SetColor();
        StartCoroutine(WaitThenDissappear());
    }

    public void SetColor()
    {
        Color numbersColor = new Color(195f / 255f, 1, 194f / 255f, _alpha * _knipper);
        Color acceptingColor = new Color(195f/255f,1,194f/255f,_alpha );

        _numbersText.color = numbersColor;
        _acceptingText.color = acceptingColor;
    }


    public string GetAcceptingString()
    {
        StringBuilder builder = new StringBuilder();

        for (int i = 0; i < _acceptingNumbers.Count; i++)
        {
            int nr = _acceptingNumbers[i];
            builder.Append(nr);
            if (i < _acceptingNumbers.Count - 1) builder.Append(", ");
        }

        return builder.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var other = collision.gameObject;
        if (other.name.Contains("Die")) Press();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var other = collision.gameObject;
        if (other.name.Contains("Die")) UnPress();
    }

    public void Press()
    {
        _alpha = 0;
        _spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f, 1);
        SetColor();
    }

    public void UnPress()
    {
        _alpha = 1;
        _spriteRenderer.color = new Color(1,1,1, 1);
        SetColor();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
