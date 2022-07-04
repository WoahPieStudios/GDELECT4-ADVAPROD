using System;
using System.Collections;
using Enums;
using Interface;
using Spawning.Scripts.Containers;
using Spawning.Scripts.Enemies;
using Spawning.Scripts.Managers;
using Spawning.Scripts.Pools;
using UnityEngine;
using Random = UnityEngine.Random;
using AdditiveScenes.Scripts.ScriptableObjects;
using Handlers;
using AdditiveScenes.Scripts.Managers;

namespace Spawning.Scripts.Spawners
{
    /// <summary>
    /// Responsible for spawning <see cref="Drone">Drones</see> into the scene.
    /// </summary>
    public class DroneSpawner : MonoBehaviour, IEnemy
    {
        [Header("Debugging")]
        [SerializeField] private bool isSpawning;
        [HideInInspector] public bool isInitialized;

        [Header("Drone Spawn Settings")]
        /// <summary>
        /// The radius of the area which the drones will spawn at the <see cref= "SpawnPoint"/>.
        /// </summary>
        [SerializeField, Tooltip("The radius of the area which the drones will spawn at the spawn point.")]
        private float spawnRadius;
        [SerializeField] private VFXHandler droneSpawnVFX;
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

        [Header("Drone Spawn Amounts")]
        [SerializeField] private int spawnAmount;
        [SerializeField] private int spawnAmountTank;
        [SerializeField, Range(0f, 1f)] private float tankSpawnChance;

        /// <summary>
        /// References the transform of the player which is used by the drones.
        /// </summary>
        [Header("Player Reference")]
        private Transform _playerTransform;


        [Header("Combat")]
        [SerializeField] private EnemyType enemyType;
        [SerializeField] private int scoreAmount;
        [SerializeField] private Weakpoint[] weakpoints;
        
        
        [Header("SFX")]
        [SerializeField] private AudioSource audioSource;
        [SerializeField] SFXChannel explosionChannel;
        [SerializeField] private VFXHandler explosionVFX;
        [SerializeField] SFXChannel SpawnSFX;
        [SerializeField] RandomSFXChannel randomTotemSfx;
        [SerializeField] private PauseEventChannel pauseEventChannel;
        [SerializeField] private SFXVolumeChannel sfxVolumeChannel;

        private float maxHealth;
        public float Health
        {
            get
            {
                var health = 0f;
                foreach (var weakpoint in weakpoints)
                {
                    health += weakpoint.Health;
                }
                return health;
            }
            set {
                foreach (var weakpoint in weakpoints)
                {
                    value += weakpoint.Health;
                }
            }
        }

        public int ScoreAmount { get => scoreAmount; set => scoreAmount = value; }

        public SpawnPoint SpawnerPoint { get; set; }

        [SerializeField] private Renderer renderer;
        private Material _material;

        private void Reset()
        {
            spawnInterval = 1f;
            spawnRadius = 1f;
        }

        private void Awake()
        {
            FindPlayerTransform();
        }

        private void Start()
        {
            maxHealth = Health;
            _material = renderer != null ? renderer.material : GetComponent<Renderer>().material;
            if (!isSpawning) return;
            if (audioSource == null) audioSource = GetComponent<AudioSource>();
            // TODO: Find a way to use the event channel for subscribing properly
            // Needs to use the actual PauseManager class to subscribe because using the event channel brings issues in unsubscribing
            PauseManager.onPause += PauseAudio;
            PauseManager.onResume += ResumeAudio;
            // pauseEventChannel.AddPauseListener(() => PauseAudio());
            // pauseEventChannel.AddResumeListener(() => ResumeAudio());
            randomTotemSfx?.PlayAudio(audioSource);
            StartCoroutine(SpawnDrone());
        }

        /// <summary>
        /// Sets the <see cref="_playerTransform"/> by looking for objects with the tag <c>Player</c>.
        /// </summary>
        private void FindPlayerTransform() => _playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;

        /// <summary>
        /// Gets a <see cref="Drone"/> from the <see cref="dronePool"/> and sets its position within the <see cref="spawnerPosition"/> and <see cref="spawnRadius"/>.
        /// </summary>
        private IEnumerator SpawnDrone()
        {
            while (!GameManager.Instance.IsGameOver)
            {
                yield return new WaitForSeconds(spawnInterval);
                for (int i = 0; i < spawnAmount; i++)
                {
                    var spawnPosition = Random.insideUnitSphere * spawnRadius + SpawnPoint;
                    var vfx = Instantiate(droneSpawnVFX, spawnPosition, Quaternion.identity, transform);
                    yield return new WaitForSeconds(vfx.particleSystem.main.duration);
                    SpawnSFX?.PlayAudio();
                    var drone = DronePool.Instance.GetDrone(EnemyType.Drone);
                    if (drone == null)
                    {
                        Debug.LogError("No more available drones");
                        yield return null;
                    }
                    drone.transform.position = spawnPosition;
                    drone.SetPlayerTransform(_playerTransform);
                    drone.SetPlayerLookState(true);
                    drone.isInitialized = true;
                    //Destroy(vfx.gameObject);
                }

                var _tankSpawnChance = Random.Range(0f, 1f);
                if (_tankSpawnChance <= tankSpawnChance)
                {
                    for (int i = 0; i < spawnAmountTank; i++)
                    {
                        var spawnPosition = Random.insideUnitSphere * spawnRadius + SpawnPoint;
                        var vfx = Instantiate(droneSpawnVFX, spawnPosition, Quaternion.identity, transform);
                        yield return new WaitForSeconds(vfx.particleSystem.main.duration);
                        SpawnSFX?.PlayAudio();
                        var drone = DronePool.Instance.GetDrone(EnemyType.Tank);
                        if (drone == null)
                        {
                            Debug.LogError("No more available drones");
                            yield return null;
                        }

                        drone.transform.position = spawnPosition;
                        drone.SetPlayerTransform(_playerTransform);
                        drone.SetPlayerLookState(true);
                        drone.isInitialized = true;
                        //Destroy(vfx.gameObject);
                    }
                }
            }
        }

        private void Update()
        {
            audioSource.volume = sfxVolumeChannel.GetVolume;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(SpawnPoint, spawnRadius);
        }

        public void TakeDamage(float damageAmount)
        {
            Health -= damageAmount;
            var color = Color.Lerp(Color.black, Color.white, Health / maxHealth);
            _material.color = color;
            if (Health <= 0) { GetDestroyed(); }
        }

        private void OnDisable()
        {
            PauseManager.onPause -= PauseAudio;
            PauseManager.onResume -= ResumeAudio;
            GetDestroyed();
        }

        public void GetDestroyed(bool killedByPlayer = true)
        {
            if (!isInitialized) return;
            isInitialized = false;
            Instantiate(explosionVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
            //SpawnerPoint.StartCooldown();
            SpawnerPoint.FreePointPosition();
            TotemSpawnManager.OnSpawnEvent();
            if (killedByPlayer)
            {
                ScoreManager.OnAddScore(ScoreAmount, EnemyType);
                explosionChannel?.PlayAudio();
            }
        }

        private void ResumeAudio()
        {
            randomTotemSfx.ResumeAudio(audioSource);
        }

        private void PauseAudio()
        {
            randomTotemSfx.PauseAudio(audioSource);
        }

        public EnemyType EnemyType
        {
            get => enemyType;
            set => enemyType = value;
        }
    }
}
