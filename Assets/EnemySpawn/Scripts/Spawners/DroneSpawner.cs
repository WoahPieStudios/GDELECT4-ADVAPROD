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
        [SerializeField] private bool collectionCheck;
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
            collectionCheck = true;
            defaultCapacity = 50;
            maxSize = 200;
            
            spawnInterval = 1f;
            spawnRadius = 1f;
        }

        private void Awake()
        {
            InitializePool();
            FindPlayerTransform();
        }

        private void InitializePool()
        {
            _dronePool = new ObjectPool<Drone>(
                CreateDrone,
                OnDroneGet,
                OnDroneRelease,
                OnDroneDestroy,
                collectionCheck,
                defaultCapacity,
                maxSize
            );
        }

        private void OnDroneDestroy(Drone drone)
        {
            Destroy(drone.gameObject);
        }

        private void OnDroneRelease(Drone drone)
        {
            drone.gameObject.SetActive(false);
        }

        private void OnDroneGet(Drone drone)
        {
            drone.transform.position = Random.insideUnitSphere * spawnRadius + spawnPoint.position;
            drone.gameObject.SetActive(true);
        }

        private Drone CreateDrone()
        {
            return Instantiate(dronePrefab, transform);
        }

        private void Start()
        {
            StartCoroutine(SpawnDrones());
        }
        

        private void FindPlayerTransform() => _playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        
        private IEnumerator SpawnDrones()
        {
            while (_playerTransform != null)
            {
                var drone = _dronePool.Get();
                drone.SetPlayerTransform(_playerTransform);
                drone.SetPool(_dronePool);
                yield return new WaitForSeconds(spawnInterval);
            }
        }
        
        private void OnDrawGizmosSelected()
        {
            if (spawnPoint == null) return;
            Gizmos.DrawWireSphere(spawnPoint.position, spawnRadius);
        }
    }
}
