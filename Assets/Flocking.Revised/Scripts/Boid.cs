using System;
using UnityEngine;

namespace Flocking.Revised.Scripts
{
    [RequireComponent(typeof(Collider))]
    public class Boid : MonoBehaviour
    {
        [SerializeField] private float neighborRadius;
        [SerializeField, Range(0f,1f)] private float avoidanceRange;

        private BoidPool _pool;
        private Collider _collider;

        private void Reset()
        {
            neighborRadius = 1f;
            avoidanceRange = 0.5f;

            _collider = GetComponent<Collider>();
        }

        private void Start()
        {
            _collider = GetComponent<Collider>();
        }

        public void Initialize(BoidPool pool, Vector3 position, Quaternion rotation)
        {
            _pool = pool;
            transform.position = position;
            transform.rotation = rotation;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.collider.CompareTag("Player")) { _pool.Pool.Release(this); }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position,neighborRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, neighborRadius * avoidanceRange);
        }
    }
}