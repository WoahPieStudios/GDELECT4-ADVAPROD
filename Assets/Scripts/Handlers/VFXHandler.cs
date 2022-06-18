using System;
using Spawning.Scripts.Pools;
using UnityEngine;

namespace Handlers
{
    public class VFXHandler : MonoBehaviour
    {
        public ParticleSystem particleSystem;

        private void Reset()
        {
            particleSystem = GetComponent<ParticleSystem>();
        }

        private void Awake()
        {
            particleSystem ??= GetComponent<ParticleSystem>();
        }

        private void OnDisable()
        {
            DronePool.Instance.Release(this);
        }
    }
}