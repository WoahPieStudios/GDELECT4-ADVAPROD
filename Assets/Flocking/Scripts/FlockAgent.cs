using System;
using UnityEngine;

namespace BoardToBits.Flocking.Scripts
{
    [RequireComponent(typeof(Collider))]
    public class FlockAgent : MonoBehaviour
    {
        private Flock _agentFlock;
        public Flock AgentFlock => _agentFlock;

        private Collider _agentCollider;
        public Collider AgentCollider => _agentCollider;

        [SerializeField] private float health = 1f;

        private Transform _agentTransform;

        private void Start()
        {
            _agentCollider = GetComponent<Collider>();
            _agentTransform = transform;
        }

        public void Initialize(Flock flock)
        {
            _agentFlock = flock;
        }

        public void Move(Vector3 velocity)
        {
            _agentTransform.forward = velocity;
            _agentTransform.position += velocity * Time.deltaTime;
        }

        public void TakeDamage(float damageAmount)
        {
            health -= damageAmount;
            if (health <= 0) { GetDestroyed(); }
        }

        private void GetDestroyed()
        {
            _agentFlock.RemoveAgent(this);
            Destroy(gameObject);
        }
    }
}
