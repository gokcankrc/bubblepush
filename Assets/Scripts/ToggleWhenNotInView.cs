using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ToggleWhenNotInView : MonoBehaviour
{
    [FormerlySerializedAs("rbs")] public List<Rigidbody2D> rigidbodies;
    public List<GameObject> gameObjects;

    private void OnBecameVisible()
    {
        foreach (var rb in rigidbodies)
        {
            rb.isKinematic = false;
        }

        foreach (var go in gameObjects)
        {
            go.SetActive(true);
        }
    }

    private void OnBecameInvisible()
    {
        foreach (var rb in rigidbodies)
        {
            rb.isKinematic = true;
        }

        foreach (var go in gameObjects)
        {
            go.SetActive(false);
        }
    }
}