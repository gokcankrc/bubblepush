using System;
using Ky;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool Started { get; private set; }
    public bool IsRunning { get; private set; }
    public bool Locked { get; private set; } = false;

    private void Start()
    {
        LevelLoaded();
        AudioManager.I.PlayMenuSound();
    }

    private void Update()
    {
        if (Locked) return;
        if (Started) return;
        if (!Input.anyKeyDown) return;

        GameplayManager.I.StartGame();
        UIManager.I.CloseAllUI();
        Started = true;
        IsRunning = true;
    }

    public void LevelLoaded()
    {
        Started = false;
        UIManager.I.ShowPressAnyKeyUI();
    }

    public void WinConditionMet()
    {
        if (!IsRunning) return;
        Debug.Log("Game Won");
        GameplayManager.I.GameEnded();
        IsRunning = false;
        UIManager.I.ShowWinUI();
    }

    public void LoseConditionMet()
    {
        if (!IsRunning) return;
        Debug.Log("Game Lost");
        GameplayManager.I.GameEnded();
        IsRunning = false;
        UIManager.I.ShowLoseUI();
    }

    public void RestartLevel()
    {
        GameplayManager.I.Clear();
        UIManager.I.CloseAllUI();
        LevelManager.I.RestartLevel();
    }

    public void NextLevel()
    {
        GameplayManager.I.Clear();
        UIManager.I.CloseAllUI();
        LevelManager.I.LoadNextLevel();
    }

    public void Lock()
    {
        Locked = true;
    }

    public void Unlock()
    {
        Locked = false;
    }
}