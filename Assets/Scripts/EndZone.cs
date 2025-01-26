using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other);
        var bubble = other.GetComponentInParent<Bubble>();
        if (bubble)
        {
            GameManager.I.WinConditionMet();
            return;
        }

        if (other.TryGetComponent(out BubbleJointBridge bridge))
        {
            if (bridge.Bubble)
            {
                GameManager.I.WinConditionMet();
            }
        }
    }
}