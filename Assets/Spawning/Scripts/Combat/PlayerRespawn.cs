using System;
using Spawning.Scripts.Managers;
using UnityEngine;

namespace Spawning.Scripts.Combat
{
    public class PlayerRespawn : MonoBehaviour
    {
        [SerializeField] private float boundsDamageAmount = 25f;
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Bounds"))
            {
                GetComponent<PlayerCombat>().TakeDamage(boundsDamageAmount);
                PlayerSpawnManager.OnRespawnPlayer();
            }
        }
    }
}