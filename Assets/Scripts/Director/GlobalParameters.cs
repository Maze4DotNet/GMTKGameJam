using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GlobalParameters : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)] public float _requiredPushingTime = 0.5f;
    [SerializeField, Range(0f, 10f)] public float _rollDuration = 0.1f;
    [SerializeField, Range(0f, 100f)] public float _rollSpeed = 0.5f;

}
