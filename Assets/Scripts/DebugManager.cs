using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugManager : MonoBehaviour
{
    [SerializeField] private KeyCode restartSceneKey = KeyCode.F5;

    private void Update()
    {
        if (Input.GetKeyDown(restartSceneKey))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}