using UnityEngine;

namespace EnemyNavigation.Scripts.Flocking
{
    public abstract class FilteredFlockBehavior : FlockBehavior
    {
        public ContextFilter filter;
    }
}