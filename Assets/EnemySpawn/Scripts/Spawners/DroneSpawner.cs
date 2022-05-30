using System.Collections;
using EnemySpawn.Scripts.Enemies;
using EnemySpawn.Scripts.Pools;
using UnityEngine;
using UnityEngine.Events;

namespace EnemySpawn.Scripts.Spawners
{
    /// <summary>
    /// Responsible for spawning <see cref="Drone">Drones</see> into the scene.
    /// </summary>
    [RequireComponent(typeof(DronePool))]
    public class DroneSpawner : MonoBehaviour, IDamageable
    {
        /// <summary>
        /// Reference to the pool this spawner will be using.
        /// </summary>
        [Header("Drone Pool"), SerializeField, Tooltip("Reference to the pool this spawner will be using.")]
        private DronePool dronePool;

        /// <summary>
        /// The radius of the area which the drones will spawn at the <see cref= "SpawnPoint"/>.
        /// </summary>
        [SerializeField, Tooltip("The radius of the area which the drones will spawn at the spawn point.")]
        private float spawnRadius;

        /// <summary>
        /// Determines the location where the drones will spawn.
        /// </summary>
        [SerializeField, Tooltip("Determines the location where the drones will spawn.")]
        private Vector3 spawnPointOffset;
        private Vector3 SpawnPoint => transform.position + spawnPointOffset;
        
        /// <summary>
        /// The frequency of when drones will be spawned.
        /// </summary>
        [SerializeField, Tooltip("The frequency of when drones will be spawned.")]
        private float spawnInterval;


        [SerializeField] private int spawnAmount;

        /// <summary>
        /// References the transform of the player which is used by the drones.
        /// </summary>
        [Header("Player Reference")]
        private Transform _playerTransform;

        [Header("Combat")]
        [SerializeField] private float health; 
        public float Health { get => health; set => health = value; }


        private void Reset()
        {
            dronePool = GetComponent<DronePool>();
            spawnInterval = 1f;
            spawnRadius = 1f;
            health = 1f;
        }

        private void Awake()
        {
            FindPlayerTransform();
        }

        private void Start()
        {
            InvokeRepeating(nameof(SpawnDrone), spawnInterval, spawnInterval);
        }

        /// <summary>
        /// Sets the <see cref="_playerTransform"/> by looking for objects with the tag <c>Player</c>.
        /// </summary>
        private void FindPlayerTransform() => _playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;

        /// <summary>
        /// Gets a <see cref="Drone"/> from the <see cref="dronePool"/> and sets its position within the <see cref="SpawnPoint"/> and <see cref="spawnRadius"/>.
        /// </summary>
        private void SpawnDrone()
        {
            for (int i = 0; i < spawnAmount; i++)
            {
                var drone = dronePool.Pool.Get();
                drone.transform.position = Random.insideUnitSphere * spawnRadius + SpawnPoint;
                drone.SetPlayerTransform(_playerTransform);
                drone.SetPlayerCollider(_playerTransform.GetComponent<Collider>());
                drone.SetPool(dronePool.Pool);
                drone.SetPlayerLookState(true);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(SpawnPoint, spawnRadius);
        }

        public void TakeDamage(float damageAmount)
        {
            Health -= damageAmount;
            if (Health <= 0) { GetDestroyed(); }
        }

        public void GetDestroyed()
        {
            Destroy(gameObject);
        }
    }
}
