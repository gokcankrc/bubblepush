using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugManager : MonoBehaviour
{
    [SerializeField] private KeyCode restartSceneKey = KeyCode.F5;
    [SerializeField] private KeyCode mainMenuKey = KeyCode.F5;
    [SerializeField] private KeyCode stopTimeKey = KeyCode.B;

    [SerializeField] private KeyCode loseKey = KeyCode.L;
    [SerializeField] private KeyCode winKey = KeyCode.O;

    private void Update()
    {
        if (Input.GetKeyDown(restartSceneKey))
        {
            GameplayManager.I.GameEnded();
            LevelManager.I.RestartLevel();
        }
        
        if (Input.GetKeyDown(mainMenuKey))
        {
            GameplayManager.I.GameEnded();
            LevelManager.I.StartMainMenu();
        }
#if UNITY_EDITOR
        if (Input.GetKeyDown(loseKey))
        {
            GameManager.I.LoseConditionMet();
        }

        if (Input.GetKeyDown(winKey))
        {
            GameManager.I.WinConditionMet();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyDown(stopTimeKey))
        {
            Time.timeScale = (1f - Time.timeScale);
        }
#endif
    }
}