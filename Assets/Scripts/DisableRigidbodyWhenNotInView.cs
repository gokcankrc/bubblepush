using System.Collections.Generic;
using UnityEngine;

public class DisableRigidbodyWhenNotInView : MonoBehaviour
{
    public List<Rigidbody2D> rbs;

    private void OnBecameVisible()
    {
        foreach (var rb in rbs)
        {
            if (rb != null)
                rb.isKinematic = false;
        }
    }

    private void OnBecameInvisible()
    {
        foreach (var rb in rbs)
        {
            if (rb != null)
                rb.isKinematic = true;
        }
    }
}