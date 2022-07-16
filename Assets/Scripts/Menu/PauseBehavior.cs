using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseBehavior : MonoBehaviour
{
    public bool _paused;
    public Canvas _canvas;
    // Start is called before the first frame update
    void Start()
    {
        _canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown ("escape")) {
            if(_paused) Unpause();
            else Pause();
        }
        if (Input.GetKeyDown ("r")) Restart();
    }

    public void Pause()
    {
        _paused = true;
        _canvas.enabled = true;
        Time.timeScale = 0f;
    }

    public void Unpause()
    {
        _paused = false;
        Time.timeScale = 1f;
        _canvas.enabled = false;
    }

    public void LevelSelect()
    {
        Unpause();
        SceneManager.LoadScene("LevelSelect");
    }

    public void Restart()
    {
        Unpause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    public void Quit()
    {
        Application.Quit();
    }
}
