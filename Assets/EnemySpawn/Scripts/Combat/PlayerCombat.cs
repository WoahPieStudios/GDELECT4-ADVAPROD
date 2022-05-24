using UnityEngine;
using UnityEngine.Events;

namespace EnemySpawn.Scripts.Combat
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] private float health;
        [SerializeField] private UnityEvent onPlayerDeath;

        private void Reset()
        {
            health = 5f;
        }

        private void OnEnable()
        {
            onPlayerDeath.AddListener(DeathSequence);
        }

        public void TakeDamage(float damageAmount)
        {
            health -= damageAmount;
            if (health <= 0){onPlayerDeath?.Invoke();}
        }

        private void DeathSequence()
        {
            Destroy(gameObject);
        }
    }
}