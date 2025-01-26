using System;
using System.Collections;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject BubblePrefab;
    public Transform BubbleContainer;

    private bool _isSpawning = false;

    public IEnumerator Start()
    {
        GameManager.I.Lock();
        yield return null;
        UIManager.I.CloseAllUI();
        GameplayManager.I.Player.Activate();
        Cursor.visible = false;
    }

    private void OnDestroy()
    {
        GameManager.I.Unlock();
    }

    private void Update()
    {
        if (_isSpawning)
        {
            _isSpawning = false;
            return;
        }

        if (GameplayManager.I.ActiveBubbles.Count <= 0)
        {
            Instantiate(BubblePrefab, BubbleContainer.position, Quaternion.identity);
            _isSpawning = true;
        }
    }

    public void PlayGame()
    {
        LevelManager.I.StartFirstLevel();
    }

    public void PlayGameAlt()
    {
        LevelManager.I.StartAltLevel();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}