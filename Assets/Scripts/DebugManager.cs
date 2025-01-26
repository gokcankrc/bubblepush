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
            GameplayManager.I.GameEnded();
            LevelManager.I.RestartLevel();
            
        }
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

#if UNITY_EDITOR
        if (Input.GetKeyDown(stopTimeKey))
        {
            Time.timeScale = (1f - Time.timeScale);
        }
#endif
    }
}