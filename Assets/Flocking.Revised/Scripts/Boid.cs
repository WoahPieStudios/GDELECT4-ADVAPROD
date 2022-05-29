using System;
using System.Collections.Generic;
using UnityEngine;

namespace Flocking.Revised.Scripts
{
    [RequireComponent(typeof(Collider))]
    public class Boid : MonoBehaviour
    {
        [SerializeField] private float maxSpeed;
        [SerializeField] private float neighborRadius;
        [SerializeField, Range(0f,1f)] private float avoidanceRange;
        [SerializeField, Range(0f,100f)] private float driveFactor;
        [SerializeField, Range(0f, 1f)] private float agentSmoothTime;

        private BoidPool _pool;
        private Collider _collider;
        private Vector3 _currentVelocity;
        public Collider Collider => _collider;

        public float MaxSpeed => maxSpeed;
        public float NeighborRadius => neighborRadius;
        
        public float SqrMaxSpeed => maxSpeed * maxSpeed;
        public float SqrNeighborRadius => neighborRadius * neighborRadius;
        public float SqrAvoidanceRange => SqrNeighborRadius * avoidanceRange * avoidanceRange;

        private void Reset()
        {
            maxSpeed = 1f;
            neighborRadius = 2f;
            avoidanceRange = 0.85f;
            driveFactor = 10f;
            agentSmoothTime = 0.5f;
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

        public void Move(List<Transform> nearbyObjects)
        {
            Vector3 velocity = Vector3.zero;
            velocity += Cohesion(nearbyObjects) + Alignment(nearbyObjects) + Avoidance(nearbyObjects);
            velocity *= driveFactor;
            if (velocity.sqrMagnitude > SqrMaxSpeed)
                velocity = velocity.normalized * MaxSpeed;

            transform.forward = velocity;
            transform.position += velocity * Time.deltaTime;
        }


        private Vector3 Cohesion(List<Transform> nearbyObjects)
        {
            var movement = Vector3.zero;
            if (nearbyObjects.Count == 0) return movement;

            foreach (var nearbyObject in nearbyObjects)
            {
                movement += nearbyObject.position;
            }

            movement /= nearbyObjects.Count;

            transform.position -= movement;
            
            movement = Vector3.SmoothDamp(transform.forward, movement, ref _currentVelocity, agentSmoothTime);

            return movement;
        }

        private Vector3 Alignment(List<Transform> nearbyObjects)
        {
            var movement = transform.forward;
            if (nearbyObjects.Count == 0) return movement;

            foreach (var nearbyObject in nearbyObjects)
            {
                movement += nearbyObject.forward;
            }

            movement /= nearbyObjects.Count;

            return movement.normalized;
        }

        private Vector3 Avoidance(List<Transform> nearbyObjects)
        {
            var movement = Vector3.zero;
            if (nearbyObjects.Count == 0) return movement;

            int objectsToAvoid = 0;

            foreach (var nearbyObject in nearbyObjects)
            {
                if(Vector3.SqrMagnitude(nearbyObject.position - transform.position) < SqrAvoidanceRange)
                {
                    objectsToAvoid++;
                    movement += transform.position - nearbyObject.position;
                }
            }

            if(objectsToAvoid > 0){
                movement /= objectsToAvoid;
            }

            return movement;
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