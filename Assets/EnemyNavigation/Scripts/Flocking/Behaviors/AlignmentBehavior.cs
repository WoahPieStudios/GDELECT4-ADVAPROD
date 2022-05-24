using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyNavigation.Scripts.Flocking
{
    [CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]
    public class AlignmentBehavior : FlockBehavior
    {
        public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
        {
            // if no neighbors, maintain current alignment
            if (context.Count == 0) return agent.transform.forward;

            // add all points together and average
            Vector3 alignmentMove = Vector3.zero;

            foreach (var item in context)
            {
                alignmentMove += item.transform.forward;
            }

            alignmentMove /= context.Count;

            return alignmentMove;
        }
    }
}