using System;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public Rigidbody2D rb;
    
    
    private Vector3 previousMousePosition;

    public float RotationSpeed = 10f;

    private void Start()
    {
        Vector3 screenPosition = Input.mousePosition;
        previousMousePosition = Camera.main.ScreenToWorldPoint(screenPosition);;
    }

    private void FixedUpdate()
    {
        Vector3 screenPosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        // Rotate cursor
        var delta = previousMousePosition - worldPosition;
        transform.right = -Vector3.Lerp(-transform.right, delta.normalized, RotationSpeed * Time.deltaTime);
        previousMousePosition = worldPosition;
        
        worldPosition.z = 0;
        rb.velocity = Vector3.zero;
        rb.MovePosition(worldPosition);
    }
}