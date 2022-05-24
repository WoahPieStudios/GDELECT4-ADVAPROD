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
    public class DroneSpawner : MonoBehaviour
    {
        /// <summary>
        /// Reference to the pool this spawner will be using.
        /// </summary>
        [Header("Drone Pool"), SerializeField, Tooltip("Reference to the pool this spawner will be using.")]
        private DronePool dronePool;

        /// <summary>
        /// Determines whether the spawner is active.
        /// </summary>
        [Header("Spawn Properties"), SerializeField, Tooltip("Determines whether the spawner is active.")]
        private bool isActive;
        /// <summary>
        /// Determines the location where the drones will spawn.
        /// </summary>
        [SerializeField, Tooltip("Determines the location where the drones will spawn.")]
        private Transform spawnPoint;
        /// <summary>
        /// The frequency of when drones will be spawned.
        /// </summary>
        [SerializeField, Tooltip("The frequency of when drones will be spawned.")]
        private float spawnInterval;

        /// <summary>
        /// The radius of the area which the drones will spawn at the <see cref= "spawnPoint"/>.
        /// </summary>
        [SerializeField, Tooltip("The radius of the area which the drones will spawn at the spawn point.")]
        private float spawnRadius;

        /// <summary>
        /// References the transform of the player which is used by the drones.
        /// </summary>
        [Header("Player Reference")]
        private Transform _playerTransform;

        [Header("Combat")]
        [SerializeField] private float health;
        
        [Header("Events")]
        [SerializeField] private UnityEvent onTotemDeath;

        private void Reset()
        {
            dronePool = GetComponent<DronePool>();
            isActive = true;
            spawnInterval = 1f;
            spawnRadius = 1f;
        }

        private void Awake()
        {
            onTotemDeath.AddListener(DisableDrones);
            FindPlayerTransform();
        }

        private void Start()
        {
            StartCoroutine(SpawnDrones());
        }

        /// <summary>
        /// Sets the <see cref="_playerTransform"/> by looking for objects with the tag <c>Player</c>.
        /// </summary>
        private void FindPlayerTransform() => _playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;

        /// <summary>
        /// Gets a <see cref="Drone"/> from the <see cref="dronePool"/> and sets its position within the <see cref="spawnPoint"/> and <see cref="spawnRadius"/>.
        /// </summary>
        private IEnumerator SpawnDrones()
        {
            while (health > 0 && _playerTransform)
            {
                SpawnDrone();
                yield return new WaitForSeconds(spawnInterval);
            }
            onTotemDeath?.Invoke();
        }

        /// <summary>
        /// Gets a <see cref="Drone"/> from the <see cref="dronePool"/> and sets its position within the <see cref="spawnPoint"/> and <see cref="spawnRadius"/>.
        /// </summary>
        private void SpawnDrone()
        {
            var drone = dronePool.Pool.Get();
            drone.transform.position = Random.insideUnitSphere * spawnRadius + spawnPoint.position;
            drone.SetPlayerTransform(_playerTransform);
            drone.SetPlayerCollider(_playerTransform.GetComponent<Collider>());
            drone.SetPool(dronePool.Pool);
            drone.SetPlayerLookState(true);
        }

        /// <summary>
        /// Flips the current state of <see cref="isActive"/>.
        /// </summary>
        public void ToggleSpawn() => isActive = !isActive;

        /// <summary>
        /// Sets <see cref="isActive"/> according to the bypass value.
        /// </summary>
        /// <param name="bypassValue">The state that will be set to <see cref="isActive"/></param>
        public void ToggleSpawn(bool bypassValue) => isActive = bypassValue;

        private void OnDrawGizmosSelected()
        {
            if (spawnPoint == null) return;
            Gizmos.DrawWireSphere(spawnPoint.position, spawnRadius);
        }

        public void TakeDamage(float damageAmount)
        {
            health -= damageAmount;
            if (health <= 0){onTotemDeath?.Invoke();}
        }
        
        private void DisableDrones()
        {
            Destroy(gameObject);
        }
    }
}
