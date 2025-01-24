using System;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public Rigidbody2D rb;
    private void Update()
    {

    }

    private void FixedUpdate()
    {        Vector3 screenPosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        worldPosition.z = 0;
        
        // transform.position = worldPosition;
        rb.velocity = Vector3.zero;
        rb.MovePosition(worldPosition);
    }
}