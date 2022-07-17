using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiGuyZoneScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var other = collision.gameObject;
        if (other.name == "AntiGuyZoneDetection")
        {
            var playerMovement = other.GetComponentInParent<PlayerMovement>();
            playerMovement.GuyDie();
        }
    }
}
