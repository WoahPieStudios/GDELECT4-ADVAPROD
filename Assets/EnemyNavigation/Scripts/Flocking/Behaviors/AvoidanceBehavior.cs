using System.Collections.Generic;
using UnityEngine;

namespace EnemyNavigation.Scripts.Flocking.Behaviors
{
    [CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
    public class AvoidanceBehavior : FilteredFlockBehavior
    {
        public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
        {
            // if no neighbors, return no adjustment
            if (context.Count == 0) return Vector3.zero;

            // add all points together and average
            Vector3 avoidanceMove = Vector3.zero;

            int nAvoid = 0;
            List<Transform> filteredContext = filter == null ? context : filter.Filter(agent, context);
            foreach (var item in filteredContext)
            {
                if (Vector3.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius)
                {

                    nAvoid++;
                    avoidanceMove += agent.transform.position - item.position;
                }
            }

            if (nAvoid > 0)
            {
                avoidanceMove /= nAvoid;
            }

            return avoidanceMove;
        }
    }
}