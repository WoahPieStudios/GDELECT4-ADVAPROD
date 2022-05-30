using UnityEngine;

namespace EnemySpawn.Scripts.Managers
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private GameObject objectToSpawn;
        [SerializeField] private SpawnPointManager spawnPointManager;

        private void Start()
        {
            SpawnObject();
        }

        public virtual void SpawnObject()
        {
            var spawnPoint = spawnPointManager.Points[Random.Range(0, spawnPointManager.Points.Count)]; 
            var obj = Instantiate(objectToSpawn, spawnPoint.TakePointPosition(), Quaternion.identity, transform);
            
        }

    }
}
