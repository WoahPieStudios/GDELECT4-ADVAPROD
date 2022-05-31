using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawning.Scripts.Managers
{
    public class PlayerSpawnManager : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private SpawnPointManager spawnPointManager;
        public static event Action RespawnPlayer;

        private void Reset()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        private void Awake()
        {
            RespawnPlayer += SetPlayerPosition;
        }

        private void SetPlayerPosition()
        {
            player.transform.position = spawnPointManager.GetRandomPoint().GetPointPosition();
        }

        public static void OnRespawnPlayer()
        {
            RespawnPlayer?.Invoke();
        }
    }
}