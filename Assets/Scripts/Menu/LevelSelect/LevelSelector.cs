using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public int level;
    public Text levelText;
    // Start is called before the first frame update
    void Start()
    {
        levelText.text = level.ToString();
    }

    public void OpenScene()
    {
        string levelString = GetLevelString(level);
        SceneManager.LoadScene(levelString);
    }

    public static string GetLevelString(int lvl)
    {
        int lastLevel = 15;
        if (lvl > lastLevel) return "LevelEnd";
        else if (lvl < 10) return $"Level0{lvl}";
        else return $"Level{lvl}";
    }
}
