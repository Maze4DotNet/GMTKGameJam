using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectorScript : MonoBehaviour
{
    public GameObject _spawnManager;
    public GameObject _uiManager;
    public GameObject _camera;
    public GameObject _canvas;

    public UIManagerScript UIManagerScript
    {
        get
        {
            return _uiManager.GetComponent<UIManagerScript>();
        }
    }
    public SpawnManagerScript SpawnManagerScript
    {
        get
        {
            return _spawnManager.GetComponent<SpawnManagerScript>();
        }
    }
    public CameraScript CameraScript
    {
        get
        {
            return _camera.GetComponent<CameraScript>();
        }
    }
}
