using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyNavigation.Scripts.Flocking.Behaviors
{
    [CreateAssetMenu(menuName = "Flock/Behavior/Steered Cohesion")]
    public class SteeredCohesionBehavior : FlockBehavior
    {

        private Vector3 _currentVelocity;
        [SerializeField] private float agentSmoothTime = 0.5f;

        public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
        {
            // if no neighbors, return no adjustment
            if (context.Count == 0) return Vector3.zero;

            // add all points together and average
            Vector3 cohesionMove = Vector3.zero;

            foreach (var item in context)
            {
                cohesionMove += item.position;
            }

            cohesionMove /= context.Count;

            // create offset from agent position
            cohesionMove -= agent.transform.position;

            cohesionMove = Vector3.SmoothDamp(agent.transform.forward, cohesionMove, ref _currentVelocity, agentSmoothTime);

            return cohesionMove;
        }
    }
}
