using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerScript : MonoBehaviour
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
