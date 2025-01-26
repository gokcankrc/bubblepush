using System.Collections;
using System.Collections.Generic;
using Ky;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
        GameManager.I.LevelLoaded();
    }

    public void StartFirstLevel()
    {
        SceneManager.LoadScene((1) % SceneManager.sceneCountInBuildSettings);
        GameManager.I.LevelLoaded();
    }
    public void StartAltLevel()
    {
        SceneManager.LoadScene((2) % SceneManager.sceneCountInBuildSettings);
        GameManager.I.LevelLoaded();
    }
    
    public void RestartLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
        GameManager.I.LevelLoaded();
    }

}
