using System.Collections;
using Spawning.Scripts.Pools;
using UnityEngine;

namespace Handlers
{
    public class VFXHandler : MonoBehaviour
    {
        public bool isStandalone;
        public ParticleSystem particleSystem;

        private void Reset()
        {
            particleSystem = GetComponent<ParticleSystem>();
        }

        private void Awake()
        {
            particleSystem ??= GetComponent<ParticleSystem>();
        }

        private void OnEnable()
        { 
            StartCoroutine(DestroyAfterLifetime());
        }

        private IEnumerator DestroyAfterLifetime()
        {
            yield return new WaitForSeconds(particleSystem.main.duration);
            if (isStandalone)
            {
                Destroy(gameObject);
            }
            else
            {
                DronePool.Instance.Release(this);
            }
        }

    }
}