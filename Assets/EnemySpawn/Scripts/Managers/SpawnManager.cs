using System;
using EnemySpawn.Scripts.Containers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EnemySpawn.Scripts.Managers
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private GameObject objectToSpawn;
        [SerializeField] private SpawnPoints spawnPoints;

        private void Start()
        {
            SpawnObject();
        }

        public virtual void SpawnObject()
        {
            var obj = Instantiate(objectToSpawn, spawnPoints.Points[Random.Range(0, spawnPoints.Points.Count)].position, Quaternion.identity, transform);
        }

    }
}
