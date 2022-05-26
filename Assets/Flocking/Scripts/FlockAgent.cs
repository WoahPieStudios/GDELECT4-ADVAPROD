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
        
    }
}
