using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardToBits.Flocking.Scripts
{
    public class Flock : MonoBehaviour
    {
        [SerializeField] private FlockAgent agentPrefab;
        private List<FlockAgent> _agents = new List<FlockAgent>();
        [SerializeField] private FlockBehavior behavior;

        [Range(10, 500), SerializeField]
        private int startingCount = 250;
        [Range(0f, 1f)]
        public float AgentDensity = 0.08f;

        [Range(1f, 100f), SerializeField]
        private float driveFactor = 10f;
        [Range(1f, 100f), SerializeField]
        private float maxSpeed = 5f;
        [Range(1f, 10f), SerializeField]
        private float neighborRadius = 1.5f;
        [Range(0f, 1f), SerializeField]
        private float avoidanceRadiusMultiplier = 0.5f;
        [SerializeField] float spawnInterval;

        private float _squareMaxSpeed;
        private float _squareNeighborRadius;
        private float _squareAvoidanceRadius;
        public float SquareAvoidanceRadius => _squareAvoidanceRadius;

        // Start is called before the first frame update
        void Start()
        {
            _squareMaxSpeed = maxSpeed * maxSpeed;
            _squareNeighborRadius = neighborRadius * neighborRadius;
            _squareAvoidanceRadius = _squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
            StartCoroutine(SpawnAgents());
        }

        private IEnumerator SpawnAgents()
        {
            while (gameObject.activeSelf)
            {
                for (int i = 0; i < startingCount; i++)
                {
                    FlockAgent agent = Instantiate(agentPrefab,
                        Random.insideUnitSphere * startingCount * AgentDensity + transform.position,
                        Quaternion.Euler(Vector3.forward * Random.Range(0, 360f)),
                        transform
                    );
                    agent.name = $"Agent {i}";
                    agent.Initialize(this);
                    _agents.Add(agent);
                }
                yield return new WaitForSeconds(spawnInterval);
            }
        }

        // Update is called once per frame
        void Update()
        {
            foreach (var agent in _agents)
            {
                List<Transform> context = GetNearbyObjects(agent);

                // agent.GetComponent<Renderer>().material.color = Color.Lerp(Color.white, Color.red, context.Count / 6f);

                Vector3 move = behavior.CalculateMove(agent, context, this);

                move *= driveFactor;

                if (move.sqrMagnitude > _squareMaxSpeed)
                {
                    move = move.normalized * maxSpeed;
                }

                agent.Move(move);
            }
        }

        private List<Transform> GetNearbyObjects(FlockAgent agent)
        {
            List<Transform> context = new List<Transform>();
            Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighborRadius);

            foreach (var c in contextColliders)
            {
                if (c != agent.AgentCollider)
                {
                    context.Add(c.transform);
                }
            }

            return context;
        }

        public void RemoveAgent(FlockAgent agent)
        {
            _agents.Remove(agent);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, AgentDensity * startingCount);
        }
    }
}
