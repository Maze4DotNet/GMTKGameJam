using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableObjectHitboxScript : MonoBehaviour
{
    public PushDetection _pushDetectionOfParent;

    public bool IsActive { get; private set; }

    private void CheckIfThisIsPlayer(Collider2D collision, bool enter)
    {
        var other = collision.gameObject;
        if (other.tag == "Players") IsActive = enter;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckIfThisIsPlayer(collision, true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CheckIfThisIsPlayer(collision, false);
    }
}
