using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Camera mainCamera;
    public Transform blade;
    [Range(0, 1)] public float edgeThreshold;
    public float moveSpeed;

    void FixedUpdate()
    {
        if (IsBladeUnderCursor())
        {
            Vector3 cameraMovement = Vector3.zero;
            if (Input.mousePosition.x > Screen.width * edgeThreshold)
            {
                //Edge Right
                cameraMovement.x += moveSpeed;
            }

            if (Input.mousePosition.x < Screen.height * (1 - edgeThreshold))
            {
                //Edge Left
                cameraMovement.x -= moveSpeed;
            }

            if (Input.mousePosition.y > Screen.height * edgeThreshold)
            {
                //Edge Top
                cameraMovement.y += moveSpeed;
            }

            if (Input.mousePosition.y < Screen.height * (1 - edgeThreshold))
            {
                //Edge Bottom
                cameraMovement.y -= moveSpeed;
            }

            mainCamera.transform.position += cameraMovement * Time.fixedDeltaTime;
        }

        Vector3 camMovement = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
        {
            camMovement.x -= 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            camMovement.x += 1;
        }

        if (Input.GetKey(KeyCode.W))
        {
            camMovement.y += 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            camMovement.y -= 1;
        }

        mainCamera.transform.position += camMovement.normalized * (moveSpeed * Time.fixedDeltaTime);
    }

    private bool IsBladeUnderCursor()
    {
        return true;
    }
}