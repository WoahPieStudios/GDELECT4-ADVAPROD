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
            if (isStandalone)
                StartCoroutine(DestroyAfterLifetime());
        }

        private IEnumerator DestroyAfterLifetime()
        {
            yield return new WaitForSeconds(particleSystem.main.duration);
            Destroy(gameObject);
        }

        private void OnDisable()
        {
            if(!isStandalone)
                DronePool.Instance.Release(this);
        }
    }
}