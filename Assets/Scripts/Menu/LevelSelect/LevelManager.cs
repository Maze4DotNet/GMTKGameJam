using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    int levelsUnlocked;

    public Button[] _buttons;
    // Start is called before the first frame update
    void Start()
    {
        levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 1);

        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].interactable = false;
        }
        for (int i = 0; i < levelsUnlocked; i++)
        {
            _buttons[i].interactable = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
