using UnityEngine;
namespace EnemyNavigation.Scripts.Flocking
{
    [RequireComponent(typeof(Collider))]
    public class FlockAgent : MonoBehaviour
    {
        private Collider _agentCollider;
        public Collider AgentCollider => _agentCollider;

        private Transform _agentTransform;
        
        private void Start()
        {
            _agentCollider = GetComponent<Collider>();
            _agentTransform = transform;
        }

        public void Move(Vector3 velocity)
        {
            _agentTransform.forward = velocity;
            _agentTransform.position += velocity * Time.deltaTime;
        }
        
    }
}
