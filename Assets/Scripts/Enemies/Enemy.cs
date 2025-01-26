using System;
using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        public Shooter Shooter;
        public TargetSelector TargetSelector;
        public float ShootCooldown;
        public GameObject idleParticle, chargeParticle;

        
        public float LastShootTime { get; private set; }

        public GameplayManager GameplayManager { get; private set; }

        private void OnCollisionEnter(Collision other)
        {
            Debug.Log(other);
            if (other.collider.TryGetComponent(out IShootable shootable))
            {
                shootable.Shot();
            }
        }

        private void Start()
        {
            GameplayManager = GameplayManager.I;
            LastShootTime = 0f;
        }

        private void Update()
        {
            if (!TargetSelector.HasTarget()) 
            {
                idleParticle.gameObject.SetActive(true);
                return; 
            }

            idleParticle.gameObject.SetActive(false);

            var target = TargetSelector.GetTarget();

            transform.right = target.transform.position - transform.position;

            if (Time.time < LastShootTime + ShootCooldown) return;
            
            
            
            Shooter.Shoot(target);
            LastShootTime = Time.time;
        }

        public void BlowUp()
        {
            Destroy(gameObject);
        }
    }
}