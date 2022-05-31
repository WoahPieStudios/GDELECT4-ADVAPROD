using UnityEngine;

namespace Spawning.Scripts.Containers
{
    public class SpawnPoint : MonoBehaviour
    {
        public bool IsTaken { get; private set; }
        /// <summary>
        /// Returns the position of this spawn point and sets <see cref="IsTaken"/> to true.
        /// </summary>
        /// <remarks>Use <see cref="GetPointPosition"/> if availability is NOT needed.</remarks>
        public Vector3 TakePointPosition()
        {
            IsTaken = true;
            return transform.position;
        }
        /// <summary>
        /// Returns the position of this spawn point.
        /// </summary>
        /// <remarks>Use <see cref="TakePointPosition"/> if availability is needed.</remarks>
        public Vector3 GetPointPosition() => transform.position;
        /// <summary>
        /// Sets <see cref="IsTaken"/> to false.
        /// </summary>
        public void SetFree() => IsTaken = false;
    }
}
