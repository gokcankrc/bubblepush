using System;
using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        public Shooter Shooter;
        public TargetSelector TargetSelector;
        public float ShootCooldown = 4f;
        public float ChargeDuration = 1f;
        public float RotationSpeed = 10f;
        public GameObject idleParticle, chargeParticle, cooldownParticle;
        
        public ParticleSystem DeathParticle;
        public AudioSource DeathAudio;
        
        public AudioSource ChargeAudio;
        public AudioSource ShootAudio;

        public EnemyState EnemyState { get; private set; } = EnemyState.Idle;

        public float LastShootTime { get; private set; }
        public float ChargeStartTime { get; private set; }

        public GameplayManager GameplayManager { get; private set; }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.TryGetComponent(out IShootable shootable))
            {
                shootable.Shot();
            }
        }

        private void Start()
        {
            GameplayManager = GameplayManager.I;
            LastShootTime = 0f;


            if (idleParticle)
                idleParticle.gameObject.SetActive(true);
            if (chargeParticle)
                chargeParticle.gameObject.SetActive(false);
            if (cooldownParticle)
                cooldownParticle.gameObject.SetActive(false);
        }

        private void Update()
        {
            var hasTarget = TargetSelector.HasTarget();
            if (hasTarget)
            {
                var target = TargetSelector.GetTarget();
                var delta = target.position - transform.position;
                transform.right = Vector3.Lerp(transform.right, delta.normalized, Time.deltaTime * RotationSpeed);
            }


            switch (EnemyState)
            {
                case EnemyState.Idle:
                    if (hasTarget)
                    {
                        if (CheckCooldown())
                            SetState(EnemyState.Charging);
                    }

                    break;
                case EnemyState.Charging:
                    if (hasTarget)
                    {
                        if (CheckChargeTime())
                        {
                            Shoot();
                        }
                    }
                    else
                    {
                        SetState(EnemyState.Idle);
                    }

                    break;
                case EnemyState.Cooldown:
                    if (CheckCooldown())
                    {
                        SetState(hasTarget ? EnemyState.Charging : EnemyState.Idle);
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private bool CheckCooldown()
        {
            return Time.time >= LastShootTime + ShootCooldown;
        }

        private bool CheckChargeTime()
        {
            return Time.time >= ChargeStartTime + ChargeDuration;
        }

        private void Shoot()
        {
            if (ShootAudio)
                ShootAudio.Play();
            var target = TargetSelector.GetTarget();
            transform.right = target.transform.position - transform.position;
            Shooter.Shoot(target);
            LastShootTime = Time.time;
            SetState(EnemyState.Cooldown);
        }

        private void SetState(EnemyState state)
        {
            if (EnemyState == state) return;

            switch (EnemyState)
            {
                case EnemyState.Idle:
                    
                    if (idleParticle)
                        idleParticle.gameObject.SetActive(false);
                    break;
                case EnemyState.Charging:
                    if (ChargeAudio) 
                        ChargeAudio.Stop();
                    if (chargeParticle)
                        chargeParticle.gameObject.SetActive(false);
                    break;
                case EnemyState.Cooldown:
                    if (cooldownParticle)
                        cooldownParticle.gameObject.SetActive(false);
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            switch (state)
            {
                case EnemyState.Idle:
                    if (idleParticle)
                        idleParticle.gameObject.SetActive(true);
                    break;
                case EnemyState.Charging:
                    ChargeStartTime = Time.time;
                    if (chargeParticle)
                        chargeParticle.gameObject.SetActive(true);
                    break;
                case EnemyState.Cooldown:
                    if (cooldownParticle)
                        cooldownParticle.gameObject.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }

            EnemyState = state;
        }

        public void BlowUp()
        {
            Destroy(gameObject);
        }
    }

    public enum EnemyState
    {
        Idle = 0,
        Charging = 1,
        Cooldown = 2,
    }
}