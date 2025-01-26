using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BubbleTrigger : MonoBehaviour
{
   public UnityEvent OnBubbleTriggered;

   public void TriggerBubble()
   {
      OnBubbleTriggered?.Invoke();
   }
}
