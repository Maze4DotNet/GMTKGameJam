using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject _director;
    public DirectorScript DirectorScript
    {
        get
        {
            return _director.GetComponent<DirectorScript>();
        }
    }
}
