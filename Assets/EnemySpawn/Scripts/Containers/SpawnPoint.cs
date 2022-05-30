using UnityEngine;

namespace EnemySpawn.Scripts.Containers
{
    public class SpawnPoint : MonoBehaviour
    {
        public bool IsTaken { get; private set; }
        public Vector3 TakePointPosition()
        {
            IsTaken = true;
            return transform.position;
        }
    }
}
