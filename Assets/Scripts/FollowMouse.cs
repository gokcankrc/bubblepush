using System;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public Rigidbody2D rb;

    private List<Vector3> _previousPositions = new List<Vector3>();
    private int index = 0;

    public int Count = 3;
    public float Threshold = 0.5f;
    
    private Vector3 previousMousePosition;

    public float RotationSpeed = 10f;

    
    public bool IsActive { get; private set; }

    private void Start()
    {
        Vector3 screenPosition = Input.mousePosition;
        previousMousePosition = Input.mousePosition;;

        for (int i = 0; i < Count; i++)
        {
            _previousPositions.Add(screenPosition);
        }
    }

    private void Update()
    {
        if (!IsActive) return;
        
        
        Vector3 screenPosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        worldPosition.z = 0;

        Vector3 total = Vector3.zero;
        foreach (var previousPosition in _previousPositions)
        {
            total += previousPosition;
        }

        var averagePreviousPos = total / _previousPositions.Count;
        
        var delta =  worldPosition - averagePreviousPos; 
        var deltaMagnitude = delta.magnitude;

        if (deltaMagnitude > Threshold)
        {
            _previousPositions[index] = worldPosition;
            index = (index + 1) % Count;
        }
      
        transform.right = delta;
        
        rb.velocity = Vector3.zero;
        rb.MovePosition(worldPosition);
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}