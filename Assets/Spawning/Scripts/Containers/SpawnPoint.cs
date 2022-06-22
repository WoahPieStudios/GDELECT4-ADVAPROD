using System.Collections;
using UnityEngine;

namespace Spawning.Scripts.Containers
{
    public class SpawnPoint : MonoBehaviour
    {
        public bool IsTaken { get; private set; }
        //public float CooldownTime { get; private set; }

        public void Initialize()
        {
            IsTaken = false;
        }
        
        // public void Initialize(float cdTime)
        // {
        //     StopCoroutine(CooldownRoutine());
        //     IsTaken = false;
        //     CooldownTime = cdTime;
        // }
        
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

        public void FreePointPosition() => IsTaken = false;

        // public void StartCooldown()
        // {
        //     StopCoroutine(CooldownRoutine());
        //     StartCoroutine(CooldownRoutine());
        // }

        // private IEnumerator CooldownRoutine()
        // {
        //     IsTaken = true;
        //     yield return new WaitForSeconds(CooldownTime);
        //     IsTaken = false;
        // }
    }
}
