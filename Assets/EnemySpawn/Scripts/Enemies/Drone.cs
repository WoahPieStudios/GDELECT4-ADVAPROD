using UnityEngine;
using UnityEngine.Pool;

namespace EnemySpawn.Scripts.Enemies
{
    public class Drone : MonoBehaviour
    {
        [Header("Debug")]
        [SerializeField] bool isStandalone;

        [Header("Properties")]
        [SerializeField] float movementSpeed;
        [SerializeField] LayerMask objectsToDetect;
        private Rigidbody _rigidBody;
        private Transform _transform;
        private bool _isLookingForPlayer;

        [Header("Player Reference")]
        [SerializeField] float attackDistance;
        private Transform _playerTransform;

        [Header("Drone Pool Reference")]
        private ObjectPool<Drone> _dronePool;


        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _transform = transform;

            if (isStandalone)
            {
                _playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
                _isLookingForPlayer = true;
            }
        }

        private void Reset()
        {
            movementSpeed = 1f;
            attackDistance = 1f;
        }

        private void FixedUpdate()
        {
            // Do not do anything if player is not yet found.
            if (_playerTransform == null) return;

            var collisions = Physics.OverlapSphere(transform.position, attackDistance, objectsToDetect);
            foreach (var collision in collisions)
            {
                // drone looks for player if it has not "seen" the player yet.
                _isLookingForPlayer = !collision.CompareTag("Player");
            }

            if (_isLookingForPlayer) { LookForPlayer(); }
            else { AttackPlayer(); }
        }

        /// <summary>
        /// Searches for the player in the scene.
        /// </summary>
        private void LookForPlayer()
        {
            print("Looking for Player");
            var position = _rigidBody.transform.position;
            var direction = _playerTransform.position - position;
            _rigidBody.MovePosition(position + direction.normalized * movementSpeed * Time.fixedDeltaTime);
        }

        /// <summary>
        /// Attack behavior of the drone towards the player.
        /// </summary>
        private void AttackPlayer()
        {
            print("Attacking Player");
        }

        /// <summary>
        /// Sets the player transform for this drone.
        /// </summary>
        /// <param name="playerTransform">The transform of the player.</param>
        public void SetPlayerTransform(Transform playerTransform) => _playerTransform = playerTransform;

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
            if (other.collider.CompareTag("Player"))
            {
                if (isStandalone) Destroy(gameObject);
                else { _dronePool.Release(this); }
            }
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, attackDistance);
        }
    }
}
