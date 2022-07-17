using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDisplayScript : MonoBehaviour
{
    private TextMeshProUGUI _levelDisplayText;

    // Start is called before the first frame update
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        string rawName = scene.name;
        string rawNumber = rawName.Remove(0,5);
        int levelNumber = int.Parse(rawNumber);
        _levelDisplayText = GetComponent<TextMeshProUGUI>();
        _levelDisplayText.text = $"Level {levelNumber}";
    }
}
