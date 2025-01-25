using System;
using System.Collections;
using System.Collections.Generic;
using Ky;
using UnityEngine;

public class GameplayManager : Singleton<GameplayManager>
{

    public Player Player;

    public List<Bubble> Bubbles = new List<Bubble>();


    public void AddBubble(Bubble bubble)
    {
        Bubbles.Add(bubble);
    }

    public void RemoveBubble(Bubble bubble)
    {
        Bubbles.Remove(bubble);
    }
}
