using System;
using Spawning.Scripts.Combat;
using Spawning.Scripts.Managers;
using Spawning.Scripts.Pools;
using UnityEngine;
using UnityEngine.Pool;

namespace Spawning.Scripts.Enemies
{
    /// <summary>
    /// Basic enemy type.
    /// </summary>
    public class Drone : MonoBehaviour, IDamageable, IScoreable
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
        [SerializeField] private float health;
        [SerializeField] private float _damageAmount;
        [SerializeField] float attackDistance;

        [Header("Player Reference")]
        private Transform _playerTransform;
        private Collider _playerCollider;

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

            if (!isStandalone) return;
            
            _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            _playerCollider = _playerTransform.GetComponent<Collider>();
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
        /// Sets the reference of player collider for this drone.
        /// </summary>
        /// <param name="playerCollider">The collider of the player.</param>
        /// <remarks>
        /// The collider of the player is needed in order to quickly accurately compute the distance between the drone and the player.
        /// </remarks>
        public void SetPlayerCollider(Collider playerCollider) => _playerCollider = playerCollider;

        /// <summary>
        /// Sets whether the drone should be looking for the player.
        /// </summary>
        /// <param name="isLookingForPlayer">The state of the drone.</param>
        public void SetPlayerLookState(bool isLookingForPlayer) => _isLookingForPlayer = isLookingForPlayer;

        private void OnCollisionEnter(Collision other)
        {
            if (!other.collider.CompareTag("Player")) return;
            
            other.collider.GetComponent<PlayerCombat>().TakeDamage(_damageAmount);
            
            GetDestroyed();
        }

        public float Health { get => health; set => health = value; }

        public void TakeDamage(float damageAmount)
        {
            health -= damageAmount;
            if (health <= 0){GetDestroyed();}
        }

        private void OnDisable()
        {
            GetDestroyed();
        }

        public void GetDestroyed()
        {
            if(isInitialized){
                ScoreManager.OnAddScore(scoreAmount);
            }
            isInitialized = false;
            if (isStandalone) Destroy(gameObject);
            else { DronePool.Instance.Release(this); }
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
    }
}
