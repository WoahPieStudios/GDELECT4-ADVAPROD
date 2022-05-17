using UnityEngine;

namespace EnemySpawn.Scripts.Enemies
{
    public class Drone : MonoBehaviour
    {

        [Header("Properties")]
        [SerializeField] float movementSpeed;
        private Rigidbody _rigidBody;
        
        [Header("Player Reference")]
        private Transform _playerTransform;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
        }

        private void Reset()
        {
            movementSpeed = 1f;
        }

        private void FixedUpdate()
        {
            // Do not do anything if player is not yet found.
            if (_playerTransform == null) return;

            AttackPlayer();
        }

        /// <summary>
        /// Looks for the player and tries to hit it.
        /// </summary>
        private void AttackPlayer()
        {
            var position = _rigidBody.transform.position;
            var direction = _playerTransform.position - position;
            _rigidBody.MovePosition(position + direction * movementSpeed * Time.fixedDeltaTime);
        }

        /// <summary>
        /// Sets the player transform for this drone.
        /// </summary>
        /// <param name="playerTransform">The transform of the player.</param>
        public void SetPlayerTransform(Transform playerTransform)
        {
            _playerTransform = playerTransform;
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
    }
}
