﻿using System.Collections.Generic;
using UnityEngine;

namespace EnemyNavigation.Scripts.Flocking
{
    public abstract class ContextFilter : ScriptableObject
    {
        public abstract List<Transform> Filter(FlockAgent agent, List<Transform> original);
    }
}