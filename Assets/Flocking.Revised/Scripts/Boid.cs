using System;
using System.Collections.Generic;
using UnityEngine;

namespace Flocking.Revised.Scripts
{
    [RequireComponent(typeof(Collider))]
    public class Boid : MonoBehaviour, IDamageable
    {
        [SerializeField] private float health;
        [SerializeField] float avoidanceRadius;
        [SerializeField] float maxVelocity;
        [SerializeField] Vector3 direction;

        private BoidPool _pool;
        private Collider _collider;
        private Transform _target;
        private LayerMask _layersToAvoid;

        private void Reset()
        {
            health = 1f;
            avoidanceRadius = 2f;
            maxVelocity = 5f;
        }
        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        public void Initialize(BoidPool pool, Vector3 position, Quaternion rotation, LayerMask layersToAvoid, Transform target)
        {
            _pool = pool;
            transform.position = position;
            transform.rotation = rotation;
            _layersToAvoid = layersToAvoid;
            _target = target;
        }

        private void Update()
        {
            direction = _target.position - transform.position;

            var collisions = Physics.OverlapSphere(transform.position, avoidanceRadius, _layersToAvoid);
            if (collisions.Length != 0)
            {
                foreach (var collision in collisions)
                {
                    if (collision != _collider)
                    {
                        Debug.DrawLine(transform.position, collision.transform.position, Color.red);
                        direction += transform.position - collision.transform.position;
                    }
                }
                direction /= collisions.Length;
            }

            transform.LookAt(transform.position);
            transform.position += direction * Time.deltaTime;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, avoidanceRadius);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                TakeDamage(health);
            }
        }

        public void TakeDamage(float damageAmount)
        {
            health -= damageAmount;
            if (health <= 0)
            {
                DestroyBoid();
            }
        }

        private void DestroyBoid()
        {
            _pool.Pool.Release(this);
        }
    }
}