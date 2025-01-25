using System.Linq;
using UnityEngine;

public class PopThatBubble : MonoBehaviour
{
    public Transform rigidbodyParent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"DEATH by {other.name}, {other.gameObject.layer}");
        Destroy(transform.parent.gameObject);

        DestroyEffects();
    }

    private void DestroyEffects()
    {
        GameObject[] joints = rigidbodyParent.Cast<Transform>().Select(x => x.gameObject).ToArray();
        int count = joints.Length;
        for (int i = 0; i < count; i++)
        {
            GameObject current = joints[i];
            DeleteJoints(current);
            ActivateSprite(current);
        }

        DeactivateMainSprite();
        Impulse();
    }

    private void DeleteJoints(GameObject current)
    {
        SpringJoint2D[] oldJoints = current.GetComponents<SpringJoint2D>();
        foreach (var joint in oldJoints)
        {
            DestroyImmediate(joint);
        }
    }

    private void ActivateSprite(GameObject current)
    {
        current.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void DeactivateMainSprite() { }
    private void Impulse() { }
}