using System.Collections.Generic;
using UnityEngine;

namespace Flocking.Revised.Scripts
{
    public class BoidSpawner : MonoBehaviour, IDamageable
    {
        [Header("Debugging")]
        [SerializeField] private bool isRepeating;

        [Header("Spawn Settings")]
        [SerializeField] private BoidPool pool;
        [SerializeField] private float spawnRadius;
        [SerializeField] private int spawnAmount;
        [SerializeField] private float spawnRate;
        [SerializeField] private Vector3 offset;

        [Header("Totem Settings")]
        [SerializeField] private float totemHealth;

        [Header("Boid Settings")]
        [SerializeField] private LayerMask layersToAvoid;

        [Header("Follow Settings")]
        [SerializeField] private bool useTag;
        [SerializeField] private string tagOfTarget;
        [SerializeField] private Transform targetToFollow;

        private void Reset()
        {
            pool = FindObjectOfType<BoidPool>();
            spawnRadius = 1f;
            spawnAmount = 10;
            spawnRate = 1f;
            totemHealth = 1f;
            layersToAvoid = 1 << 6 | 1 << 7;
            useTag = true;
            tagOfTarget = "Player";
        }

        private void Awake()
        {
            pool = FindObjectOfType<BoidPool>();
            targetToFollow = useTag ? GameObject.FindGameObjectWithTag(tagOfTarget).transform : targetToFollow;
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

        private void SpawnBoids()
        {
            for (int i = 0; i < spawnAmount; i++)
            {
                var boid = pool.Pool.Get();
                boid.Initialize(
                    pool: pool,
                    position: Random.insideUnitSphere * spawnRadius + transform.position + offset,
                    rotation: Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.forward),
                    layersToAvoid: layersToAvoid,
                    target: targetToFollow);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position + offset, spawnRadius);
        }

        public void TakeDamage(float damageAmount)
        {
            totemHealth -= damageAmount;
            if (totemHealth <= 0)
            {
                DestroyTotem();
            }
        }

        private void DestroyTotem()
        {
            Destroy(gameObject);
        }
    }
}