using System;
using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        public Shooter Shooter;
        public TargetSelector TargetSelector;
        public float ShootCooldown;
        
        public float LastShootTime { get; private set; }

        public GameplayManager GameplayManager { get; private set; }

        private void OnCollisionEnter(Collision other)
        {
            Debug.Log(other);
            if (other.collider.TryGetComponent(out IShootable shootable))
            {
                shootable.Shot(null, Shooter);
            }
        }

        private void Start()
        {
            GameplayManager = GameplayManager.I;
            LastShootTime = 0f;
        }

        private void Update()
        {
            if (Time.time < LastShootTime + ShootCooldown) return;

            if (!TargetSelector.HasTarget()) return;
            
            var target = TargetSelector.GetTarget();
            
            Shooter.Shoot(target);
            LastShootTime = Time.time;
        }

        public void BlowUp()
        {
            Destroy(gameObject);
        }
    }
}