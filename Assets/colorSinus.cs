using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorSinus : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // Assign the SpriteRenderer in the Inspector or let it auto-detect.
    public float frequency = 1f; // Controls the speed of the oscillation.
    public float minAlpha = 0.2f; // Minimum alpha value.
    public float maxAlpha = 1f; // Maximum alpha value.

    private float time;

    void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (spriteRenderer == null)
        {
            Debug.LogWarning("SpriteRenderer is not assigned or found!");
        }
    }

    void Update()
    {
        if (spriteRenderer != null)
        {
            // Calculate the alpha using a sinusoidal wave.
            time += Time.deltaTime * frequency;
            float alpha = Mathf.Lerp(minAlpha, maxAlpha, (Mathf.Sin(time) + 1f) / 2f);

            // Update the SpriteRenderer's color.
            Color color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;
        }
    }
}
