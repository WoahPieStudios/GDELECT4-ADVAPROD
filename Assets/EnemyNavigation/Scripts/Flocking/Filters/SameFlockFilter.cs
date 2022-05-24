﻿using System.Collections.Generic;
using UnityEngine;

namespace EnemyNavigation.Scripts.Flocking.Filters
{
    [CreateAssetMenu(menuName = "Flock/Filter/Same Flock")]
    public class SameFlockFilter : ContextFilter
    {
        public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
        {
            List<Transform> filtered = new List<Transform>();
            foreach (var item in original)
            {
                FlockAgent itemAgent = item.GetComponent<FlockAgent>();
                if (itemAgent != null && itemAgent.AgentFlock == agent.AgentFlock)
                {
                    filtered.Add(item);
                }
            }
            return filtered;
        }
    }
}