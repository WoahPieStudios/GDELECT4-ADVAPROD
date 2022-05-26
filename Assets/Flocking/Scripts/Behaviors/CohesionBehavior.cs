using System.Collections.Generic;
using UnityEngine;

namespace BoardToBits.Flocking.Scripts.Behaviors
{
    [CreateAssetMenu(menuName = "Flock/Behavior/Cohesion")]
    public class CohesionBehavior : FilteredFlockBehavior
    {
        public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
        {
            // if no neighbors, return no adjustment
            if (context.Count == 0) return Vector3.zero;

            // add all points together and average
            Vector3 cohesionMove = Vector3.zero;
            List<Transform> filteredContext = filter == null ? context : filter.Filter(agent, context);
            foreach (var item in filteredContext)
            {
                cohesionMove += item.position;
            }

            cohesionMove /= context.Count;

            // create offset from agent position
            cohesionMove -= agent.transform.position;

            return cohesionMove;
        }
    }
}
