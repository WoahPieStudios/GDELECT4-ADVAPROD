using System.Collections.Generic;
using UnityEngine;

namespace BoardToBits.Flocking.Scripts
{
    public abstract class FlockBehavior : ScriptableObject
    {
        public abstract Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock);
    }
}
