using System;
using System.Collections.Generic;
using UnityEngine;

public class BubblePuller : MonoBehaviour
{
    public AudioSource AudioSource;
    public float PullRange = 10f;
    public float MinRange = 2f;
    public float PullForce = 1f;
    public float MaxPullVelocity = 1f;

    private List<Bubble> _bubblesInRange = new List<Bubble>();

    public GameplayManager GameplayManager { get; private set; }

    public bool IsActive { get; private set; }
    
    public bool IsPulling { get; private set; }

    private void Start()
    {
        GameplayManager = GameplayManager.I;
    }

    private void Update()
    {
        if (!IsActive) return;
        
        FilterBubbles();
        HandleBubblesInRange();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            PullStart();
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            PullBubbles();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            PullEnd();
        }
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }


    private void PullStart()
    {
        IsPulling = true;
        foreach (var bubble in _bubblesInRange)
        {
            bubble.PullStart();
        }
        
        if (AudioSource)
            AudioSource.Play();
    }

    private void PullEnd()
    {
        IsPulling = false;
        foreach (var bubble in _bubblesInRange)
        {
            bubble.PullEnd();
        }
        
        if (AudioSource)
            AudioSource.Stop();
    }

    private void PullBubbles()
    {
        foreach (var bubble in _bubblesInRange)
        {
            var direction = (transform.position - bubble.transform.position).normalized;
            bubble.Pull(direction, PullForce, MaxPullVelocity);
        }
    }

    private void HandleBubblesInRange()
    {
        var pullRangeSqr = PullRange * PullRange;
        var minRangeSqr = MinRange * MinRange;
        var bubbles = GameplayManager.ActiveBubbles;

        var position = transform.position;

        foreach (var bubble in bubbles)
        {
            var delta = bubble.transform.position - position;
            var sqrMagnitude = delta.sqrMagnitude;

            if (sqrMagnitude <= pullRangeSqr && sqrMagnitude >= minRangeSqr)
            {
                AddBubble(bubble);
            }
            else
            {
                if (!IsPulling)
                {
                    RemoveBubble(bubble);
                }
            }
        }
    }

    private void FilterBubbles()
    {
        for (int i = _bubblesInRange.Count - 1; i >= 0; i--)
        {
            if (!_bubblesInRange[i] || !_bubblesInRange[i].CanPull)
                _bubblesInRange.RemoveAt(i);
        }
    }

    private void AddBubble(Bubble bubble)
    {
        if (_bubblesInRange.Contains(bubble)) return;
        _bubblesInRange.Add(bubble);
        bubble.InRange();
    }

    private void RemoveBubble(Bubble bubble)
    {
        if (!_bubblesInRange.Contains(bubble)) return;
        _bubblesInRange.Remove(bubble);
        bubble.OutOfRange();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, PullRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, MinRange);
    }
}