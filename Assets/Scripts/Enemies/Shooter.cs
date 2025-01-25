using System;
using UnityEngine;

namespace Enemies
{
    public class Shooter : MonoBehaviour
    {
        public Transform ShootPosition;
        public Projectile ProjectilePrefab;
        public float ShootPositionDistance = 1f;


        public void Shoot(Vector3 direction, Vector3 position)
        {

            var projectile = Instantiate(ProjectilePrefab, position, Quaternion.identity);

            projectile.Init(this);

            projectile.Shoot(direction);
            
            Debug.Log(projectile, projectile);

        }


        public void Shoot(Transform target)
        {
            var targetVector = target.position - ShootPosition.position;

            var direction = targetVector.normalized;
            ShootPosition.localPosition = direction * ShootPositionDistance;

            Shoot(direction, ShootPosition.position);
        }

    
    }
}