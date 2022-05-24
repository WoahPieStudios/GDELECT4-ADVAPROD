using System.Collections.Generic;
using UnityEngine;

namespace EnemyNavigation.Scripts.Flocking
{
    public abstract class FlockBehavior : ScriptableObject
    {
        public abstract Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock);
    }
}
