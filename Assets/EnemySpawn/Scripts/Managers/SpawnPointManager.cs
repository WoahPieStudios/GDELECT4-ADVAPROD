using System.Collections.Generic;
using System.Linq;
using EnemySpawn.Scripts.Containers;
using UnityEngine;

namespace EnemySpawn.Scripts.Managers
{
    public class SpawnPointManager : MonoBehaviour
    {
        [SerializeField] private List<SpawnPoint> points;
        public List<SpawnPoint> Points => points;
        public List<SpawnPoint> TakenPoints => TakenPoints.Where(p => p.IsTaken).ToList();

        private void Reset()
        {
            points = GetComponentsInChildren<SpawnPoint>().ToList();
        }
    }
}
