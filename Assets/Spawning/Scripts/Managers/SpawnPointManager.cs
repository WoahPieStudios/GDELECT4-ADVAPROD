using System;
using System.Collections.Generic;
using System.Linq;
using Spawning.Scripts.Containers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawning.Scripts.Managers
{
    public class SpawnPointManager : MonoBehaviour
    {
        [SerializeField] private List<SpawnPoint> points;
        [SerializeField] private float cooldown;

        private void Reset()
        {
            cooldown = 30f;
            points = GetComponentsInChildren<SpawnPoint>().ToList();
        }

        private void Awake()
        {
            points ??= GetComponentsInChildren<SpawnPoint>().ToList();
            foreach (var point in points)
            {
                point.Initialize(cooldown);
            }
        }

        public bool AllPointsTaken()
        {
            var taken = points.Count(point => point.IsTaken);
            return taken == points.Count;
        }

        public SpawnPoint GetRandomPoint() => points[Random.Range(0, points.Count)];

    }
}
