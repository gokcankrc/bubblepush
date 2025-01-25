using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopThatBubble : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"DEATH by {other.name}, {other.gameObject.layer}");
        Destroy(transform.parent.gameObject);
    }
}