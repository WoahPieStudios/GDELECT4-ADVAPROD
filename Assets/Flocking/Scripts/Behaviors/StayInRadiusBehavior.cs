using System.Collections.Generic;
using UnityEngine;

namespace BoardToBits.Flocking.Scripts.Behaviors
{
    [CreateAssetMenu(menuName = "Flock/Behavior/Stay in Radius")]
    public class StayInRadiusBehavior : FlockBehavior
    {
        public Vector3 center;
        public float radius = 15f;
        
        public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
        {
            Vector3 centerOffset = center - agent.transform.position;
            float t = centerOffset.magnitude / radius;
            if (t < 0.9f)
            {
                return Vector2.zero;
            }

            return centerOffset * t * t;
        }
    }
}
