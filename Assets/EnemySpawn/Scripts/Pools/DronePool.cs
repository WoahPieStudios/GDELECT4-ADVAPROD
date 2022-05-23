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
        /// <summary>
        /// The type of <see cref="Drone"/> this pool contains.
        /// </summary>
        [Header("Drone Pool"), SerializeField, Tooltip("The type of drone this pool contains.")]
        private Drone dronePrefab;

        /// <summary>
        /// Collection checks are performed when an instance is returned back to the pool.
        /// An exception will be thrown if the instance is already in the pool.
        /// Collection checks are only performed in the Editor.
        /// </summary>
        [SerializeField, Tooltip("Collection checks are performed when an instance is returned back to the pool.")]
        private bool collectionCheck;

        /// <summary>
        /// The default capacity the stack will be created with.
        /// </summary>
        [SerializeField, Tooltip("The default capacity the stack will be created with.")]
        private int defaultCapacity;

        /// <summary>
        /// The maximum size of the pool.
        /// When the pool reaches the max size then any further instances returned to the pool will be ignored and can be garbage collected.
        /// This can be used to prevent the pool growing to a very large size.
        /// </summary>
        [SerializeField, Tooltip("The maximum size of the pool.")]
        private int maxSize;

        /// <summary>
        /// The object pool itself.
        /// </summary>
        public ObjectPool<Drone> Pool { get; private set; }

        private void Reset()
        {
            collectionCheck = true;
            defaultCapacity = 10;
            maxSize = 50;
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