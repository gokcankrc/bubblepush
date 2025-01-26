using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GrassHingeController : MonoBehaviour
{
    public List<HingeJoint2D> hingeJoints;  // Reference to the HingeJoint2D
    public GameObject brokenEffectPrefab; // Optional: visual effect when broken
    public float fadeDuration = 2f;  // Duration of the fade-out effect

    private Renderer objRenderer; // Reference to the object's renderer
    private bool isFading = false; // Flag to track if fading is in progress
    private float fadeTimer = 0f; // Timer to track fade progress
    public GameObject FakeGrass;

    void Start()
    {
        // Get the Renderer component of the object
        objRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // Check if the hinge joint is broken (connectedBody is null)
        if (!isFading && hingeJoints.Any(x => x == null || x.connectedBody == null) )
        {
            
            isFading = true;
            OnHingeBroken();
        }

        // If fading is in progress, update the fade effect
        if (isFading)
        {
            fadeTimer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, fadeTimer / fadeDuration); // Lerp from 1 (opaque) to 0 (transparent)

            // Change the material alpha to fade out
            Color currentColor = objRenderer.material.color;
            objRenderer.material.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            
            // If the fade is complete, destroy the object
            if (fadeTimer >= fadeDuration)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnHingeBroken()
    {
        // Optional: Instantiate a visual effect when the hinge breaks
        if (brokenEffectPrefab != null)
        {
            Instantiate(brokenEffectPrefab, transform.position, Quaternion.identity);
            
        }
    }
}