using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public TextMesh _acceptingText;
    public TextMesh _numbersText;
    public List<int> _acceptingNumbers;
    public SpriteRenderer _spriteRenderer;
    public SoundManager _soundManager;
    [SerializeField, Range(0f, 2f)] private float _knippertime = 0.5f;
    private int _knipper = 1;
    private int _alpha = 1;
    [SerializeField] public bool _isGreen;
    [SerializeField] public bool _isBlue;
    private float _r;
    private float _g;
    private float _b;
    [SerializeField, Range(0f,10f)] private float _celebrateTime;

    public Sprite _green;
    public Sprite _yellow;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        string acceptPart;
        if (_isGreen)
        {
            _r = 195f / 255f;
            _g = 255f / 255f;
            _b = 194f / 255f;
            acceptPart = "Accepting";
        }
        else if (_isBlue)
        {
            _r = 195f / 255f;
            _g = 194f / 255f;
            _b = 255f / 255f; 
            acceptPart = "Open On:";
        }
        else
        {
            _r = 255f / 255f;
            _g = 153f / 255f;
            _b = 146f / 255f;
            acceptPart = "Forbidden";
        }
        string numberPart = GetAcceptingString();
        _acceptingText.text = $"{acceptPart}:\n";
        _numbersText.text = $"{_acceptingText.text}{numberPart}";
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
        Color numbersColor = new Color(_r, _g, _b, _alpha * _knipper);
        Color acceptingColor = new Color(_r, _g, _b, _alpha);

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
            if (i < _acceptingNumbers.Count - 1) builder.Append(" ");
        }

        return builder.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var other = collision.gameObject;
        if (other.name.Contains("Die")) Press(other);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var other = collision.gameObject;
        if (other.name.Contains("Die")) UnPress();
    }

    public void Press(GameObject die)
    {
        _alpha = 0;
        _spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f, 1);
        SetColor();
        ActualDieScript dieScript = die.GetComponentInChildren<ActualDieScript>();
        if (dieScript is null)
        {
            print("ERROR: Die script not found. Now die.");
            return;
        }
        if (_acceptingNumbers.Contains(dieScript._die.Top))
        {
            if (_isGreen)
            {
                dieScript.Win();
                Victory(dieScript);
            }
            else if (_isBlue)
            {
                _soundManager.PlaySound("button-good");
                var blockScript = GetComponentInChildren<DoorScript>();
                blockScript.Open();
            }
            else BounceBack(dieScript);
        }
        else 
        {
                _soundManager.PlaySound("button-wrong");
        }
    }


    private void Victory(ActualDieScript dieScript)
    {
        _spriteRenderer.color = new Color(1, 1, 1, 1);
        dieScript.StopNumbers();
        TurnColorYellow();
        StartCoroutine(WaitThenChangeColorGreen());
    }


    IEnumerator WaitThenChangeColorGreen()
    {
        yield return new WaitForSeconds(_celebrateTime);
        TurnColorGreen();
        StartCoroutine(WaitThenChangeColorYellow());
    }

    private void TurnColorGreen()
    {
        _spriteRenderer.sprite = _green;
    }
    private void TurnColorYellow()
    {
        _spriteRenderer.sprite = _yellow;
    }

    IEnumerator WaitThenChangeColorYellow()
    {
        yield return new WaitForSeconds(_celebrateTime);
        TurnColorYellow();
        StartCoroutine(WaitThenChangeColorGreen());
    }

    private void BounceBack(ActualDieScript dieScript)
    {
        dieScript.BounceBackWhenDone = true;
    }

    public void UnPress()
    {
        _alpha = 1;
        _spriteRenderer.color = new Color(1, 1, 1, 1);
        SetColor();

        if (_isBlue)
        {
            var blockScript = GetComponentInChildren<DoorScript>();
            blockScript.Close();
        }
    }
}
