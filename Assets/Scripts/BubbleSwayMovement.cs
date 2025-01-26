using System.Collections.Generic;
using UnityEngine;

public class BubbleSwayMovement : MonoBehaviour
{
    public float upwardForce = 1f;
    public float swayForce = 1f;
    public float swayNoiseSpeed = 1f;

    public List<Rigidbody2D> rbs = new List<Rigidbody2D>();
    private float noiseOffset;

    void Start()
    {
        noiseOffset = Random.Range(0f, 100f);
    }

    void FixedUpdate()
    {
        Vector2 upwardMovement = Vector2.up * upwardForce;
        float noise = Mathf.PerlinNoise(Time.time * swayNoiseSpeed, noiseOffset);
        float sway = (noise - 0.5f) * 2f * swayForce;

        Vector2 swayMovement = Vector2.right * sway;

        foreach (Rigidbody2D rb in rbs)
        {
            rb.AddForce(upwardMovement + swayMovement);
        }
    }
}