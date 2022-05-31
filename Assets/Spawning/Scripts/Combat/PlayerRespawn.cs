using System;
using Spawning.Scripts.Managers;
using UnityEngine;

namespace Spawning.Scripts.Combat
{
    public class PlayerRespawn : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Bounds"))
            {
                PlayerSpawnManager.OnRespawnPlayer();
            }
        }
    }
}