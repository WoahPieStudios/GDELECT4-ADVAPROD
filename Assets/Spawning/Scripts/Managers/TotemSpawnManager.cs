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

        #region Old Spawner Script
        // public void StartSpawning()
        // {
        //     StartCoroutine(SpawnObject());
        // }
        //
        // // TODO: Change to something non-recursive in the future
        // private IEnumerator SpawnObject()
        // {
        //     yield return new WaitForSeconds(initialDelay);
        //     while (!spawnPointManager.AllPointsTaken())
        //     {
        //         print($"Spawning {objectToSpawn.name}");
        //         var point = LookForAvailablePoint();
        //         var spawner = Instantiate(objectToSpawn, point.TakePointPosition(), Quaternion.identity, transform);
        //         spawner.SpawnerPoint = point;
        //         spawner.isInitialized = true;
        //         yield return new WaitForSeconds(spawnInterval);
        //     }
        //     print("All points taken");
        //     StartCoroutine(SpawnObject());
        // }
        //
        // private SpawnPoint LookForAvailablePoint()
        // {
        //     var spawnPoint = spawnPointManager.GetRandomPoint();
        //     return !spawnPoint.IsTaken ? spawnPoint : LookForAvailablePoint();
        // }
        //
        // public void ClearAllTotems()
        // {
        //     StopCoroutine(SpawnObject());
        //     var totems = FindObjectsOfType<DroneSpawner>();
        //     if (totems.Length <= 0) return;
        //     foreach (var totem in totems)
        //     {
        //         Destroy(totem.gameObject);
        //     }
        // }
        #endregion
        
        
        
    }
}
