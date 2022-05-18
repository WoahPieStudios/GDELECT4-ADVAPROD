using System.Collections;
using EnemySpawn.Scripts.Enemies;
using EnemySpawn.Scripts.Pools;
using UnityEngine;

namespace EnemySpawn.Scripts.Spawners
{
    /// <summary>
    /// Responsible for spawning <see cref="Drone">Drones</see> into the scene.
    /// </summary>
    public class DroneSpawner : MonoBehaviour
    {
        [Header("Drone Pool")]
        [SerializeField] private DronePool dronePool;

        [Header("Spawn Properties")]
        [SerializeField] private bool isSpawning;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private float spawnInterval;
        [SerializeField] private float spawnRadius;

        [Header("Player Reference")] 
        private Transform _playerTransform;

        private void Reset()
        {
            isSpawning = true;
            spawnInterval = 1f;
            spawnRadius = 1f;
        }

        private void Awake()
        {
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
            while (isSpawning)
            {
                var drone = dronePool.Pool.Get();
                drone.transform.position = Random.insideUnitSphere * spawnRadius + spawnPoint.position;
                drone.SetPlayerTransform(_playerTransform);
                drone.SetPool(dronePool.Pool);
                yield return new WaitForSeconds(spawnInterval);
            }
        }
        
        /// <summary>
        /// Flips the current state of <see cref="isSpawning"/>.
        /// </summary>
        public void ToggleSpawn() => isSpawning = !isSpawning;
        
        /// <summary>
        /// Sets <see cref="isSpawning"/> according to the bypass value.
        /// </summary>
        /// <param name="bypassValue">The state that will be set to <see cref="isSpawning"/></param>
        public void ToggleSpawn(bool bypassValue) => isSpawning = bypassValue;
        
        private void OnDrawGizmosSelected()
        {
            if (spawnPoint == null) return;
            Gizmos.DrawWireSphere(spawnPoint.position, spawnRadius);
        }
    }
}
