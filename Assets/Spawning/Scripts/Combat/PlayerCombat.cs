using System;
using Handlers;
using UnityEngine;
using UnityEngine.Events;

namespace Spawning.Scripts.Combat
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] private float health;
        [SerializeField] private UnityEvent onPlayerDeath;
        [SerializeField] private VignetteMaterialHandler takeDamageVFX;
        public float Health => health <= 0 ? 0 : health;
        public static event Action onHealthUpdate; 

        private void Reset()
        {
            health = 5f;
        }

        private void OnEnable()
        {
            onPlayerDeath.AddListener(DeathSequence);
            onHealthUpdate += SetDamageVFX;
        }

        private void OnDisable()
        {
            onHealthUpdate -= SetDamageVFX;
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
            SetDamageVFX();
            if (health <= 0){onPlayerDeath?.Invoke();}
        }

        private void SetDamageVFX()
        {
            
        }

        private void DeathSequence()
        {
            Debug.Log("Player died");
            //Destroy(gameObject);
        }
    }
}