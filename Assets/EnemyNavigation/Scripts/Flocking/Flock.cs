using System.Collections.Generic;
using UnityEngine;

namespace EnemyNavigation.Scripts.Flocking
{
    public class Flock : MonoBehaviour
    {
        [SerializeField] private FlockAgent agentPrefab;
        private List<FlockAgent> _agents = new List<FlockAgent>();
        [SerializeField] private FlockBehavior behavior;

        [Range(10, 500), SerializeField]
        private int startingCount = 250;
        private const float AgentDensity = 0.08f;

        [Range(1f, 100f), SerializeField]
        private float driveFactor = 10f;
        [Range(1f, 100f), SerializeField]
        private float maxSpeed = 5f;
        [Range(1f, 10f), SerializeField]
        private float neighborRadius = 1.5f;
        [Range(0f, 1f), SerializeField]
        private float avoidanceRadiusMultiplier = 0.5f;

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

            for (int i = 0; i < startingCount; i++)
            {
                FlockAgent agent = Instantiate(agentPrefab,
                    Random.insideUnitSphere * startingCount * AgentDensity,
                    Quaternion.Euler(Vector3.up * Random.Range(0, 360f)),
                    transform
                );
                agent.name = $"Agent {i}";
                _agents.Add(agent);
            }
        }

        // Update is called once per frame
        void Update()
        {
            foreach (var agent in _agents)
            {
                List<Transform> context = GetNearbyObjects(agent);

                agent.GetComponent<Renderer>().material.color = Color.Lerp(Color.white, Color.red, context.Count / 6f);

                // Vector3 move = behavior.CalculateMove(agent, context, this);

                // move *= driveFactor;

                // if (move.sqrMagnitude > _squareMaxSpeed)
                // {
                //     move = move.normalized * maxSpeed;
                // }

                // agent.Move(move);
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
    }
}
