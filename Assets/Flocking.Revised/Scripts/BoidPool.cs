using UnityEngine;
using UnityEngine.Pool;

namespace Flocking.Revised.Scripts
{
    public class BoidPool : MonoBehaviour
    {
        [Header("Pool Settings")]
        [SerializeField] private Boid boidPrefab;
        [SerializeField] private bool collectionCheck;
        [SerializeField] private int defaultCapacity;
        [SerializeField] private int maxSize;

        public ObjectPool<Boid> Pool { get; private set; }

        private void Reset()
        {
            collectionCheck = true;
            defaultCapacity = 50;
            maxSize = 100;
        }

        private void Awake() { InitializePool(); }
        private void InitializePool() { Pool = new ObjectPool<Boid>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestroy, collectionCheck, defaultCapacity, maxSize); }
        private Boid CreateFunc() { return Instantiate(boidPrefab, transform); }
        private void ActionOnGet(Boid obj) { obj.gameObject.SetActive(true); }
        private void ActionOnRelease(Boid obj) { obj.gameObject.SetActive(false); }
        private void ActionOnDestroy(Boid obj) { Destroy(obj.gameObject); }


    }
}