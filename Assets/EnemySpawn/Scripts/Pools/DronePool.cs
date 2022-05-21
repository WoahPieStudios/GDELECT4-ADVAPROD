using EnemySpawn.Scripts.Enemies;
using EnemySpawn.Scripts.Spawners;
using UnityEngine;
using UnityEngine.Pool;

namespace EnemySpawn.Scripts.Pools
{
    /// <summary>
    /// A pool of <see cref="Drone"/> objects for <see cref="DroneSpawner"> Drone Spawners </see> to use.
    /// </summary>
    /// <seealso cref="ObjectPool{T}"/>
    public class DronePool : MonoBehaviour
    {
        [Header("Drone Pool")]
        [SerializeField] private Drone dronePrefab;
        [SerializeField] private bool collectionCheck;
        [SerializeField] private int defaultCapacity;
        [SerializeField] private int maxSize;
        public ObjectPool<Drone> Pool { get; private set; }

        private void Reset()
        {
            collectionCheck = true;
            defaultCapacity = 50;
            maxSize = 200;
        }

        private void Awake()
        {
            InitializePool();
        }

        private void InitializePool()
        {
            Pool = new ObjectPool<Drone>(
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
            drone.gameObject.SetActive(true);
        }

        private Drone CreateDrone()
        {
            return Instantiate(dronePrefab, transform);
        }

    }
}