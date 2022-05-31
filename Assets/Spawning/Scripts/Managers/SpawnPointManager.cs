using System.Collections.Generic;
using System.Linq;
using Spawning.Scripts.Containers;
using UnityEngine;

namespace Spawning.Scripts.Managers
{
    public class SpawnPointManager : MonoBehaviour
    {
        [SerializeField] private List<SpawnPoint> points;
        public List<SpawnPoint> Points => points;

        private void Reset()
        {
            points = GetComponentsInChildren<SpawnPoint>().ToList();
        }

        public bool AllPointsTaken()
        {
            var taken = points.Count(point => point.IsTaken);
            return taken == points.Count;
        }

    }
}
