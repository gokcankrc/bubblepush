using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BubbleTrigger : MonoBehaviour
{
    public UnityEvent OnBubbleTriggered;
    public float delay = 1f;

    public void TriggerBubble()
    {
        StartCoroutine(delayed());

        IEnumerator delayed()
        {
            yield return new WaitForSeconds(delay);
            OnBubbleTriggered?.Invoke();
        }
    }
}