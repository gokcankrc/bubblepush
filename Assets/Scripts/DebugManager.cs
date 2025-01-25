using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugManager : MonoBehaviour
{
    [SerializeField] private KeyCode restartSceneKey = KeyCode.F5;
    [SerializeField] private KeyCode stopTimeKey = KeyCode.B;

    private void Update()
    {
        if (Input.GetKeyDown(restartSceneKey))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

#if UNITY_EDITOR
        if (Input.GetKeyDown(stopTimeKey))
        {
            Time.timeScale = (1f - Time.timeScale);
        }
#endif
    }
}