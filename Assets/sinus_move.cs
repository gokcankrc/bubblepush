using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sinus_move : MonoBehaviour
{
    public float moveFrequency = 1f; // Speed of the movement oscillation.
    public float moveAmplitude = 1f; // How far it moves.
    public Vector2 movementDirection = Vector2.up; // Direction of movement.

    private Vector3 startPosition; // Initial position.

    void Start()
    {
        // Save the starting position of the object.
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the offset based on a sine wave.
        Vector3 movementOffset = movementDirection.normalized * Mathf.Sin(Time.time * moveFrequency) * moveAmplitude;

        // Apply the offset to the starting position.
        transform.position = startPosition + movementOffset;
    }
}
