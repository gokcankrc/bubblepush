using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Camera mainCamera;          
    public Transform blade;
    public float edgeThreshold;
    public float moveSpeed;

    void Update()
    {
        if (IsBladeUnderCursor())
        {
            Debug.Log("Under");
            Vector3 cameraMovement = Vector3.zero;
            if (Input.mousePosition.x > Screen.width - edgeThreshold)
            { 
                //Edge Right
                cameraMovement.x += moveSpeed; 
            }
            if (Input.mousePosition.x < edgeThreshold)
            {
                //Edge Left
                cameraMovement.x -= moveSpeed;
            }
            if (Input.mousePosition.y > Screen.height - edgeThreshold)
            {
                //Edge Top
                cameraMovement.y += moveSpeed;
            }
            if (Input.mousePosition.y < edgeThreshold)
            {
                //Edge Bottom
                cameraMovement.y -= moveSpeed;
            }

            mainCamera.transform.position += cameraMovement;

        }


        
    }
    private bool IsBladeUnderCursor()
    {
        Vector2 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hitCollider = Physics2D.OverlapPoint(mouseWorldPosition);

        // Check if the hit object is the Mermaid
        return hitCollider != null && hitCollider.transform == blade;
    }
}
