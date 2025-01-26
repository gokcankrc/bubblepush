using System.Collections;
using UnityEngine;

public class EndZone : MonoBehaviour
{
    public float delay = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(delayed());

        IEnumerator delayed()
        {
            yield return new WaitForSeconds(delay);
            WinMaybe(other);
        }
    }

    private void WinMaybe(Collider2D other)
    {
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