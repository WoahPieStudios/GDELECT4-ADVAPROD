using System;
using Enums;
using Interface;
using Spawning.Scripts.Combat;
using Spawning.Scripts.Managers;
using Spawning.Scripts.Pools;
using UnityEngine;
using UnityEngine.Pool;
using AdditiveScenes.Scripts.ScriptableObjects;

namespace Spawning.Scripts.Enemies
{
    /// <summary>
    /// Basic enemy type.
    /// </summary>
    public class Drone : MonoBehaviour, IEnemy
    {
        [Header("Debug")]
        [SerializeField] bool isStandalone;

        [Header("Properties")]
        [SerializeField] float movementSpeed;
        private Rigidbody _rigidBody;
        private Transform _transform;
        private bool _isLookingForPlayer;
        public bool isInitialized;

        [Header("Combat")]
        [SerializeField] private EnemyType enemyType;
        [SerializeField] private float health;
        [SerializeField] private float _damageAmount;
        [SerializeField] float attackDistance;
        private Material _material;
        private float maxHealth;

        [SerializeField] SFXChannel enemyDeathChannel;

        [Header("Player Reference")]
        private Transform _playerTransform;

        private void Reset()
        {
            movementSpeed = 1f;
            _damageAmount = 1f;
            attackDistance = 1f;
        }

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _transform = transform;
            _material = GetComponent<Renderer>().material;
            maxHealth = health;
            if (!isStandalone) return;

            _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            _isLookingForPlayer = true;
        }

        private void FixedUpdate()
        {
            // Do not do anything if player is not yet found.
            if (_playerTransform == null) return;

            // // Sets the state depending on the distance of the drone to the player.
            // _isLookingForPlayer = !(Vector3.Distance(_transform.position, _playerTransform.position + _playerCollider.bounds.extents) < attackDistance);
            //
            // if (_isLookingForPlayer) { LookForPlayer(); }
            // else { AttackPlayer(); }

            LookForPlayer();
        }

        /// <summary>
        /// Searches for the player in the scene.
        /// </summary>
        private void LookForPlayer()
        {
            print("Looking for Player");
            var position = _rigidBody.transform.position;
            var direction = _playerTransform.position - position;
            transform.forward = direction;
            _rigidBody.MovePosition(position + direction.normalized * (movementSpeed * Time.fixedDeltaTime));
        }

        #region AttackPlayer
        /// <summary>
        /// Attack behavior of the drone towards the player.
        /// </summary>
        private void AttackPlayer()
        {
            print("Attacking Player");
        }

        #endregion

        /// <summary>
        /// Sets the reference of player transform for this drone.
        /// </summary>
        /// <param name="playerTransform">The transform of the player.</param>
        /// <remarks>
        /// The transform of the player is needed in order for the drone to locate the player.
        /// </remarks>
        public void SetPlayerTransform(Transform playerTransform) => _playerTransform = playerTransform;

        /// <summary>
        /// Sets whether the drone should be looking for the player.
        /// </summary>
        /// <param name="isLookingForPlayer">The state of the drone.</param>
        public void SetPlayerLookState(bool isLookingForPlayer) => _isLookingForPlayer = isLookingForPlayer;

        private void OnCollisionEnter(Collision other)
        {
            if (!other.collider.CompareTag("Player")) return;

            other.collider.GetComponent<PlayerCombat>().TakeDamage(_damageAmount);

            GetDestroyed(false);
        }

        public float Health { get => health; set => health = value; }

        public void TakeDamage(float damageAmount)
        {
            health -= damageAmount;
            var color = Color.Lerp(Color.black, Color.white, Health / maxHealth);
            _material.color = color;
            if (health <= 0) { GetDestroyed(); }
        }

        private void OnEnable()
        {
            _material.color = Color.white;
        }

        public void GetDestroyed(bool killedByPlayer = true)
        {
            enemyDeathChannel?.PlayAudio();
            if (!isInitialized) return;
            if (killedByPlayer) { ScoreManager.OnAddScore(scoreAmount, EnemyType); }
            DronePool.Instance.Release(this);
            isInitialized = false;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, attackDistance);
        }

        [SerializeField] private int scoreAmount;

        public int ScoreAmount
        {
            get => scoreAmount;
            set => scoreAmount = value;
        }

        public EnemyType EnemyType
        {
            get => enemyType;
            set => enemyType = value;
        }
    }
}
