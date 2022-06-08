using Enums;
using Interface;
using Spawning.Scripts.Containers;
using Spawning.Scripts.Enemies;
using Spawning.Scripts.Managers;
using Spawning.Scripts.Pools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawning.Scripts.Spawners
{
    /// <summary>
    /// Responsible for spawning <see cref="Drone">Drones</see> into the scene.
    /// </summary>
    public class DroneSpawner : MonoBehaviour, IEnemy
    {
        [Header("Debugging")]
        [SerializeField] private bool isSpawning;

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


        [Header("Combat")] [SerializeField] private EnemyType enemyType;
        [SerializeField] private float health;
        [SerializeField] private int scoreAmount;

        private float maxHealth;
        public float Health { get => health; set => health = value; }
        public int ScoreAmount { get => scoreAmount; set => scoreAmount = value; }

        public SpawnPoint SpawnerPoint { get; set; }

        private Material _material;

        private void Reset()
        {
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
            maxHealth = health;
            _material = GetComponent<Renderer>().material;
            if (!isSpawning) return;
            InvokeRepeating(nameof(SpawnDrone), spawnInterval, spawnInterval);
        }

        /// <summary>
        /// Sets the <see cref="_playerTransform"/> by looking for objects with the tag <c>Player</c>.
        /// </summary>
        private void FindPlayerTransform() => _playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;

        /// <summary>
        /// Gets a <see cref="Drone"/> from the <see cref="dronePool"/> and sets its position within the <see cref="spawnerPosition"/> and <see cref="spawnRadius"/>.
        /// </summary>
        private void SpawnDrone()
        {
            for (int i = 0; i < spawnAmount; i++)
            {
                var drone = DronePool.Instance.GetDrone();
                if (drone == null)
                {
                    Debug.LogError("No more available drones");
                    return;
                }
                drone.transform.position = Random.insideUnitSphere * spawnRadius + SpawnPoint;
                drone.SetPlayerTransform(_playerTransform);
                drone.SetPlayerLookState(true);
                drone.isInitialized = true;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(SpawnPoint, spawnRadius);
        }

        public void TakeDamage(float damageAmount)
        {
            Health -= damageAmount;
            var color = Color.Lerp(Color.red, Color.white, Health / maxHealth);
            _material.color = color;
            if (Health <= 0) { GetDestroyed(); }
        }

        private void OnDisable()
        {
            GetDestroyed();
        }

        public void GetDestroyed()
        {
            Destroy(gameObject);
            SpawnerPoint.StartCooldown();
            ScoreManager.OnAddScore(ScoreAmount, EnemyType);
        }

        public EnemyType EnemyType
        {
            get => enemyType;
            set => enemyType = value;
        }
    }
}
