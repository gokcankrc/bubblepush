using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public FollowMouse FollowMouse;
    public BubblePuller BubblePuller;


    private void Start()
    {
        GameplayManager.I.Player = this;
    }

    public void Activate()
    {
        FollowMouse.Activate();
        BubblePuller.Activate();
    }

    public void Deactivate()
    {
        FollowMouse.Deactivate();
        BubblePuller.Deactivate();
    }
}