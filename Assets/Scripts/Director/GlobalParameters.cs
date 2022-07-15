using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GlobalParameters : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)] public float _requiredPushingTime;

}
