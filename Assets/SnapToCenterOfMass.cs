using Ky;
using UnityEngine;

public class SnapToCenterOfMass : MonoBehaviour
{
    private Vector2 centerOfMass;
    public Transform rigidbodyParent;

    private void Update()
    {
        transform.position = GetCenterOfMass().ToXY();
    }

    public Vector2 GetCenterOfMass()
    {
        if (rigidbodyParent == null) return Vector2.zero;

        centerOfMass = Vector2.zero;
        var rbs = rigidbodyParent?.GetComponentsInChildren<Rigidbody2D>();
        float totalMass = 0;
        for (int i = 0; i < rbs.Length; i++)
        {
            var part = rbs[i];
            centerOfMass += part.worldCenterOfMass * part.mass;
            totalMass += part.mass;
        }

        centerOfMass /= totalMass;
        return centerOfMass;
    }

    private void OnDrawGizmos()
    {
        GetCenterOfMass();
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, centerOfMass);
        Gizmos.DrawWireSphere(centerOfMass, 0.2f);
    }
}