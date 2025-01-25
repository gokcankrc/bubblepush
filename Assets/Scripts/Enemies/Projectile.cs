using System;
using UnityEngine;

namespace Enemies
{
    public class Projectile : MonoBehaviour
    {
        public Rigidbody2D Rigidbody2D;

        public float Speed = 5f;
        public Shooter Source { get; private set; }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.TryGetComponent(out IShootable shootable))
            {
                shootable.Shot(this, Source);
            }
            
            Destroy(gameObject);
        }

        public void Init(Shooter shooter)
        {
            Source = shooter;
        }
        
        public void Shoot(Vector3 direction)
        {
            Rigidbody2D.velocity = direction * Speed;
        }
    }
}