using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Flocking.Revised.Scripts
{
    public class BoidSpawner : MonoBehaviour
    {
        [Header("Debugging")] 
        [SerializeField] private bool isRepeating;
        
        [Header("Spawn Settings")]
        [SerializeField] private BoidPool pool;
        [SerializeField] private float spawnRadius;
        [SerializeField] private int spawnAmount;
        [SerializeField] private float spawnRate;

        [Header("Boid Settings")]
        [SerializeField] private LayerMask layersToAvoid;

        private List<Boid> boids = new List<Boid>();

        private void Reset()
        {
            pool = FindObjectOfType<BoidPool>();
            spawnRadius = 1f;
            spawnAmount = 10;
            spawnRate = 1f;
        }

        private void Awake()
        {
            pool = FindObjectOfType<BoidPool>();
        }

        private void Start()
        {
            if (pool == null)
            {
                Debug.LogError("No pool found, please ensure that a Boid Pool has been set in the scene.");
                return;
            }

            if (!isRepeating) { SpawnBoids(); }
            else { InvokeRepeating(nameof(SpawnBoids), 1f, spawnRate); }
        }

        private void Update()
        {
            foreach (var boid in boids)
            {
                var nearbyObjects = GetNearbyObjects(boid);
                boid.Move(nearbyObjects);
            }
        }

        private List<Transform> GetNearbyObjects(Boid boid)
        {
            List<Transform> context = new List<Transform>();
            Collider[] contextColliders = Physics.OverlapSphere(boid.transform.position, boid.NeighborRadius, layersToAvoid);

            foreach (var collider in contextColliders)
            {
                if (collider != boid.Collider)
                {
                    context.Add(collider.transform);
                }
            }

            return context;
        }

        private void SpawnBoids()
        {
            for (int i = 0; i < spawnAmount; i++)
            {
                var boid = pool.Pool.Get();
                boid.Initialize(
                    pool,
                    Random.insideUnitSphere * spawnRadius + transform.position,
                    Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.forward));
                boids.Add(boid);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, spawnRadius);
        }
    }
}