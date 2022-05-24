using EnemySpawn.Scripts.Combat;
using UnityEngine;
using UnityEngine.Pool;

namespace EnemySpawn.Scripts.Enemies
{
    /// <summary>
    /// Basic enemy type.
    /// </summary>
    public class Drone : MonoBehaviour
    {
        [Header("Debug")]
        [SerializeField] bool isStandalone;

        [Header("Properties")]
        [SerializeField] float movementSpeed;
        private Rigidbody _rigidBody;
        private Transform _transform;
        private bool _isLookingForPlayer;

        [Header("Combat")]
        [SerializeField] private float health;
        [SerializeField] private float _damageAmount;
        [SerializeField] float attackDistance;

        [Header("Player Reference")]
        private Transform _playerTransform;
        private Collider _playerCollider;

        [Header("Drone Pool Reference")]
        private ObjectPool<Drone> _dronePool;

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

        /// <summary>
        /// Attack behavior of the drone towards the player.
        /// </summary>
        private void AttackPlayer()
        {
            print("Attacking Player");
        }

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
        /// Sets a reference to which pool this drone belongs to.
        /// </summary>
        /// <param name="dronePool">The pool where the drone came from.</param>
        public void SetPool(ObjectPool<Drone> dronePool) => _dronePool = dronePool;

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

        public void TakeDamage(float damageAmount)
        {
            health -= damageAmount;
            if (health <= 0){GetDestroyed();}
        }
        
        public void GetDestroyed()
        {
            if (isStandalone) Destroy(gameObject);
            else { _dronePool.Release(this); }
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, attackDistance);
        }
    }
}
