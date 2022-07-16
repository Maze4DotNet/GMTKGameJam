using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelWin : MonoBehaviour
{
    public void Win()
    {
        string currentLevel = SceneManager.GetActiveScene().name.Substring(5);
        int levelNumber;
        int.TryParse(currentLevel, out levelNumber);

        if (levelNumber >= PlayerPrefs.GetInt("levelsUnlocked"))
        {
            PlayerPrefs.SetInt("levelsUnlocked", levelNumber + 1);
        }

        Debug.Log("Level " + PlayerPrefs.GetInt("levelsUnlocked") + " unlocked!");
        
        SceneManager.LoadScene("Level" + (levelNumber + 1).ToString());
    }
}
