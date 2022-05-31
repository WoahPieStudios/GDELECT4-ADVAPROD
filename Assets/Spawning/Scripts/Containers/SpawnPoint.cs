using UnityEngine;

namespace Spawning.Scripts.Containers
{
    public class SpawnPoint : MonoBehaviour
    {
        public bool IsTaken { get; private set; }
        public Vector3 TakePointPosition()
        {
            IsTaken = true;
            return transform.position;
        }
        public void SetFree()
        {
            IsTaken = false;
        }
    }
}
