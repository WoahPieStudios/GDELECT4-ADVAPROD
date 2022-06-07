using System;
using System.Collections.Generic;
using Spawning.Scripts.Combat;
using Spawning.Scripts.Pools;
using UnityEngine;

namespace Spawning.Scripts.Enemies
{
    /// <summary>
    /// Basic enemy type.
    /// </summary>
    public class Drone : MonoBehaviour, IDamageable, IScoreable
    {

        [SerializeField] private float maxHealth;
        [SerializeField] private int scoreAmount;
        [SerializeField] private float maxVelocity;
        [SerializeField] private float avoidanceRadius;
        [SerializeField] private LayerMask layersToAvoid;

        public float Health { get; set; }
        public int ScoreAmount { get; set; }

        public bool isInitialized { get; private set; }
        public Vector3 Velocity { get; private set; }
        public float SqrMagnitude => maxVelocity * maxVelocity;

        private Transform _playerTransform;
        private List<Drone> _flock;

        public void Initialize(Transform playerTransform, List<Drone> flock)
        {
            _playerTransform = playerTransform;
            _flock = flock;

            Health = maxHealth;
            ScoreAmount = scoreAmount;
            isInitialized = true;
        }

        private void Update()
        {
            Velocity += Cohesion();
            Velocity += Alignment();
            Velocity += Avoidance(Physics.OverlapSphere(transform.position, avoidanceRadius));

            if (Velocity.sqrMagnitude > SqrMagnitude)
                Velocity = Velocity.normalized * maxVelocity;

            transform.position += Velocity * Time.deltaTime;
        }

        Vector3 Cohesion()
        {
            var movement = Vector3.zero;
            if (_flock.Count <= 0) return movement;
            foreach (var drone in _flock)
            {
                movement += drone.transform.position;
            }

            movement /= _flock.Count;

            movement -= transform.position;

            return movement;
        }

        Vector3 Alignment()
        {
            var movement = transform.forward;
            if (_flock.Count <= 0) return movement;
            foreach (var drone in _flock)
            {
                movement += drone.transform.forward;
            }

            movement /= _flock.Count;

            movement -= transform.forward;

            movement.Normalize();

            return movement;
        }

        Vector3 Avoidance(Collider[] colliders)
        {
            var movement = Vector3.zero;

            if (colliders.Length <= 0) return movement;

            foreach (var collisions in colliders)
            {
                movement -= collisions.transform.position;
            }

            movement /= colliders.Length;

            movement -= transform.position;

            return movement;
        }

        Vector3 PlayerFollow()
        {
            var movement = Vector3.zero;
            if (_playerTransform == null) return movement;

            movement = _playerTransform.position - transform.position;

            movement.Normalize();

            return movement;
        }

        public void TakeDamage(float damageAmount)
        {
            Health -= damageAmount;
            if (Health <= 0)
                GetDestroyed();
        }

        public void GetDestroyed()
        {
            if (!isInitialized) return;
            isInitialized = false;
            ScoreManager.OnAddScore(ScoreAmount);
            DronePool.Instance.Release(this);
            _flock.Remove(this);
        }
    }
}
