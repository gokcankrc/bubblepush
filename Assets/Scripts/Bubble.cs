using System;
using System.Collections.Generic;
using Enemies;
using Ky;
using UnityEngine;

public class Bubble : MonoBehaviour, IShootable
{
    public PopThatBubble PopThatBubble;
    public SnapToCenterOfMass SnapToCenterOfMass;
    public float DamageRange = 1f;
    public SpriteRenderer SpriteRenderer;
    
    public List<Rigidbody2D> BoneRigidbodies = new List<Rigidbody2D>();
    
    public Color DefaultColor = Color.white;
    public Color InRangeColor = Color.red;
    public Color PullingColor = Color.green;
    
    public bool CanPull = true;
    
    public GameplayManager GameplayManager { get; private set; }

    private void Start()
    {
        GameplayManager = GameplayManager.I;
        GameplayManager.AddBubble(this);
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

    public void Shot(Projectile projectile, Shooter source)
    {
        Pop();
    }

    private void Pop()
    {
        CanPull = false;
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
        Debug.Log("Out of range");
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
    
  

}