using System;
using UnityEngine;

namespace Enemies
{
    public class Shooter : MonoBehaviour
    {
        public Transform ShootPosition;
        public Projectile ProjectilePrefab;
        public GameObject ShootParticle;


        public void Shoot(Vector3 direction, Vector3 position)
        {
            var particle = Instantiate(ShootParticle, transform.position, transform.rotation);
            var projectile = Instantiate(ProjectilePrefab, position, Quaternion.identity);

            projectile.Init(this);

            projectile.Shoot(direction);
            projectile.transform.right = direction;
        }


        public void Shoot(Transform target)
        {
            var targetVector = target.position - ShootPosition.position;

            var direction = targetVector.normalized;
           

            Shoot(direction, ShootPosition.position);
        }

    
    }
}