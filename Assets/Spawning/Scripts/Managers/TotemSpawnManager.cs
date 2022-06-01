using System.Collections;
using Spawning.Scripts.Containers;
using Spawning.Scripts.Spawners;
using UnityEngine;

namespace Spawning.Scripts.Managers
{
    public class TotemSpawnManager : MonoBehaviour
    {
        [SerializeField] private DroneSpawner objectToSpawn;
        [SerializeField] private SpawnPointManager spawnPointManager;
        [SerializeField] private float initialDelay;
        [SerializeField] private float spawnInterval;

        private void Start()
        {
            StartCoroutine(SpawnObject());
        }

        // TODO: Change to something non-recursive in the future
        private IEnumerator SpawnObject()
        {
            yield return new WaitForSeconds(initialDelay);
            while(!spawnPointManager.AllPointsTaken())
            {
                print($"Spawning {objectToSpawn.name}");
                var point = LookForAvailablePoint();
                var spawner = Instantiate(objectToSpawn, point.TakePointPosition(), Quaternion.identity, transform);
                spawner.SpawnerPoint = point;
                yield return new WaitForSeconds(spawnInterval);
            }
            print("All points taken");
            StartCoroutine(SpawnObject());
        }

        private SpawnPoint LookForAvailablePoint()
        {
            var spawnPoint = spawnPointManager.GetRandomPoint();
            return !spawnPoint.IsTaken ? spawnPoint : LookForAvailablePoint();
        }

    }
}
