using System;
using UnityEngine;
using UnityEngine.Events;

namespace Spawning.Scripts.Combat
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] private float health;
        [SerializeField] private UnityEvent onPlayerDeath;
        public float Health => health;
        public static event Action onHealthUpdate; 

        private void Reset()
        {
            health = 5f;
        }

        private void OnEnable()
        {
            onPlayerDeath.AddListener(DeathSequence);
        }

        public void ResetHealth()
        {
            health = 100f;
            onHealthUpdate?.Invoke();
        }
        
        public void TakeDamage(float damageAmount)
        {
            health -= damageAmount;
            onHealthUpdate?.Invoke();
            if (health <= 0){onPlayerDeath?.Invoke();}
        }

        private void DeathSequence()
        {
            Debug.Log("Player died");
            //Destroy(gameObject);
        }
    }
}