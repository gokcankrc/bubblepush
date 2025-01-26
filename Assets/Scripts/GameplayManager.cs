using System;
using System.Collections;
using System.Collections.Generic;
using Ky;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameplayManager : Singleton<GameplayManager>
{
    public Player Player;

    public List<Bubble> ActiveBubbles = new List<Bubble>();
    public List<Bubble> DeactivatedBubbles = new List<Bubble>();

    public bool GameIsPlaying { get; private set; }

    public void StartGame()
    {
        GameIsPlaying = true;
        Cursor.visible = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Confined;
        WarpCursorToWorldObject(Player.transform);
        Player.Activate();
    }

    public void GameEnded()
    {
        Cursor.visible = true;
        GameIsPlaying = false;
        Player.Deactivate();
    }

    private void Update()
    {
        if (!GameIsPlaying) return;
        if (ActiveBubbles.Count <= 0)
        {
            GameManager.I.LoseConditionMet();
            GameIsPlaying = false;
        }
    }

    public void AddBubble(Bubble bubble)
    {
        ActiveBubbles.Add(bubble);
    }
    
    public void AddDeactivatedBubble(Bubble bubble)
    {
        DeactivatedBubbles.Add(bubble);
    }

    public void ActivateBubble(Bubble bubble)
    {
        ActiveBubbles.Add(bubble);
        DeactivatedBubbles.Remove(bubble);
    }
    
    public void RemoveBubble(Bubble bubble)
    {
        DeactivatedBubbles.Remove(bubble);
        ActiveBubbles.Remove(bubble);
    }

    public void Clear()
    {
        ActiveBubbles.Clear();
        DeactivatedBubbles.Clear();
    }
    
    public static void WarpCursorToWorldObject(Transform transform)
    {
        if (Mouse.current == null)
        {
            return;
        }

        var camera = Camera.main;

        var screenPos = camera.WorldToScreenPoint(transform.position);

        Mouse.current.WarpCursorPosition(screenPos);
    }
}
