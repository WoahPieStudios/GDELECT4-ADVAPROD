using Spawning.Scripts.Combat;
using Spawning.Scripts.Pools;
using UnityEngine;

namespace Spawning.Scripts.Enemies
{
    /// <summary>
    /// Basic enemy type.
    /// </summary>
    public class Drone : MonoBehaviour, IDamageable, IScoreable
    {

        [SerializeField] private float maxHealth;
        [SerializeField] private int scoreAmount;
        
        public float Health { get; set; }
        public int ScoreAmount { get; set; }

        public bool isInitialized { get; private set; }

        private Transform _playerTransform;
        
        public void Initialize(Transform playerTransform)
        {
            _playerTransform = playerTransform;
            Health = maxHealth;
            ScoreAmount = scoreAmount;
            isInitialized = true;
        }
        
        public void TakeDamage(float damageAmount)
        {
            Health -= damageAmount;
            if(Health <= 0) 
                GetDestroyed();
        }
        
        public void GetDestroyed()
        {
            if (!isInitialized) return;
            ScoreManager.OnAddScore(ScoreAmount);
            isInitialized = false;
            DronePool.Instance.Release(this);
        }
    }
}
