using System;
using System.Collections.Generic;
using Enemies;
using Ky;
using UnityEngine;

public class Bubble : MonoBehaviour, IShootable
{
    public bool IsActive = true;

    public PopThatBubble PopThatBubble;
    public SnapToCenterOfMass SnapToCenterOfMass;
    public float DamageRange = 1f;
    public SpriteRenderer SpriteRenderer;

    public int DeactiveLayerIndex = 14;
    public int ActiveLayerEvenIndex = 7;
    public int ActiveLayerOddIndex = 9;
        

    public List<Rigidbody2D> BoneRigidbodies = new List<Rigidbody2D>();

    public Color DeactiveColor = Color.gray;
    public Color DefaultColor = Color.white;
    public Color InRangeColor = Color.red;
    public Color PullingColor = Color.green;

    public bool CanPull => !_popping && IsActive;
    public GameplayManager GameplayManager { get; private set; }

    private bool _popping = false;

    private void Start()
    {
        GameplayManager = GameplayManager.I;
        if (IsActive)
        {
            GameplayManager.AddBubble(this);
            SpriteRenderer.color = DefaultColor;
        }
        else
        {
            GameplayManager.AddDeactivatedBubble(this);
            SpriteRenderer.color = DeactiveColor;
            
            DeactivateBones();
        }
    }

   


    private void OnDestroy()
    {
        GameplayManager.RemoveBubble(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"DEATH by {other.name}, {other.gameObject.layer}");
        Pop();
    }

    public void Shot()
    {
        Pop();
    }

    private void Pop()
    {
        _popping = true;
        var overlapCircleAll = Physics2D.OverlapCircleAll(transform.position, DamageRange);


        foreach (Collider2D collider in overlapCircleAll)
        {
            if (collider.TryGetComponent(out Enemy enemy))
            {
                enemy.BlowUp();
            }
        }


        PopThatBubble.PopTheBubble();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, DamageRange);
    }

    public void OutOfRange()
    {
        SpriteRenderer.color = DefaultColor;
    }

    public void InRange()
    {
        SpriteRenderer.color = InRangeColor;
    }

    public void PullStart()
    {
        SpriteRenderer.color = PullingColor;
    }

    public void PullEnd()
    {
        SpriteRenderer.color = InRangeColor;
    }

    public void Pull(Vector3 direction, float pullForce, float maxVelocity)
    {
        var count = BoneRigidbodies.Count;
        var force = pullForce / count;

        Vector2 velocity = Vector2.zero;
        foreach (var rigidbody in BoneRigidbodies)
        {
            velocity += rigidbody.velocity;
        }

        var avgVelocity = velocity / count;
        var projection = MathfExtensions.Project(avgVelocity, direction);

        if (projection.sqrMagnitude >= maxVelocity * maxVelocity) return;

        foreach (var rigidbody in BoneRigidbodies)
        {
            rigidbody.AddForce(direction * force, ForceMode2D.Force);
        }
    }


    public void BridgeTriggerEnter(Collider2D other)
    {
    }

    public void BridgeTriggerExit(Collider2D other)
    {
    }

    public void BridgeCollisionEnter(Collision2D other)
    {
        if (other.collider.TryGetComponent(out BubbleJointBridge bridge))
        {
            if (bridge.Bubble.IsActive && !IsActive)
            {
                ActivateBubble();
            }
        }
    }

    public void BridgeCollisionExit(Collision2D other)
    {
    }

    private void ActivateBubble()
    {
        IsActive = true;
        SpriteRenderer.color = DefaultColor;
        GameplayManager.ActivateBubble(this);
        
        ActivateBones();
    }
    
    private void ActivateBones()
    {
        for (var i = 0; i < BoneRigidbodies.Count; i++)
        {
            var boneRigidbody = BoneRigidbodies[i];
            boneRigidbody.gameObject.layer = i % 2 == 0 ? ActiveLayerOddIndex : ActiveLayerEvenIndex;

            boneRigidbody.bodyType = RigidbodyType2D.Dynamic;
        }
    }
    private void DeactivateBones()
    {
        foreach (var boneRigidbody in BoneRigidbodies)
        {
            boneRigidbody.gameObject.layer = DeactiveLayerIndex;
            boneRigidbody.bodyType = RigidbodyType2D.Kinematic;
        }
    }

 
}