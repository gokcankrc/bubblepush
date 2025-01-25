using System;
using Enemies;
using UnityEngine;

public class Bubble : MonoBehaviour, IShootable
{
    public PopThatBubble PopThatBubble;
    public SnapToCenterOfMass SnapToCenterOfMass;
    public float DamageRange = 1f;
    
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

    public void Shot(Projectile projectile, Shooter source)
    {
        
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
}