using System.Collections;
using UnityEngine;

public class EndZone : MonoBehaviour
{
    public float delay = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var bubble = other.GetComponentInParent<Bubble>();
        if (bubble)
        {
            StartCoroutine(delayed());
            return;
        }

        if (other.TryGetComponent(out BubbleJointBridge bridge))
        {
            if (bridge.Bubble)
            {
                StartCoroutine(delayed());
            }
        }

        IEnumerator delayed()
        {
            yield return new WaitForSeconds(delay);
            WinMaybe(other);
        }
    }

    private void WinMaybe(Collider2D other)
    {
        GameManager.I.WinConditionMet();
    }
}