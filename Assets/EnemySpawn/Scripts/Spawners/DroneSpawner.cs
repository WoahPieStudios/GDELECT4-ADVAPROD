using System.Collections;
using EnemySpawn.Scripts.Enemies;
using UnityEngine;
using UnityEngine.Pool;

namespace EnemySpawn.Scripts.Spawners
{
    public class DroneSpawner : MonoBehaviour
    {
        [Header("Drone Pool")]
        [SerializeField] private Drone dronePrefab;
        [SerializeField] private int defaultCapacity;
        [SerializeField] private int maxSize;
        private ObjectPool<Drone> _dronePool;

        [Header("Spawn Properties")]
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private float spawnInterval;
        [SerializeField] private float spawnRadius;

        [Header("Player Reference")] 
        private Transform _playerTransform;

        private void Reset()
        {
            spawnInterval = 1f;
            spawnRadius = 1f;
            defaultCapacity = 20;
            maxSize = 100;
        }


        private void Awake()
        {
            InitializePool();
            FindPlayerTransform();
        }

        private void Start()
        {
            StartCoroutine(SpawnDrones());
        }

        private void InitializePool()
        {
            _dronePool = new ObjectPool<Drone>(
                () => Instantiate(dronePrefab),
                drone => drone.gameObject.SetActive(true),
                drone => drone.gameObject.SetActive(false),
                drone => Destroy(drone.gameObject),
                false,
                defaultCapacity,
                maxSize
            );
        }

        private void FindPlayerTransform() => _playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        
        private IEnumerator SpawnDrones()
        {
            if (_playerTransform == null)
            {
                Debug.LogWarning("No player found, unable to spawn drones.");
                yield break;
            }
            var drone = _dronePool.Get();
            drone.transform.position = Random.insideUnitSphere + spawnPoint.position;
            drone.SetPlayerTransform(_playerTransform);
            yield return new WaitForSeconds(spawnInterval);
        }
        
        private void OnDrawGizmosSelected()
        {
            if (spawnPoint == null) return;
            Gizmos.DrawWireSphere(spawnPoint.position, spawnRadius);
        }
    }
}
