using System;
using Spawning.Scripts.Managers;
using UnityEngine;

namespace Handlers
{
    [CreateAssetMenu(menuName = "Handlers/New PlayerSpawnManagerHandler")]
    public class PlayerSpawnManagerHandler : ScriptableObject
    {
        public void AddListener(Action listener)
        {
            PlayerSpawnManager.RespawnPlayer += listener;
        }

        public void RemoveListener(Action listener)
        {
            PlayerSpawnManager.RespawnPlayer -= listener;
        }
    }
}