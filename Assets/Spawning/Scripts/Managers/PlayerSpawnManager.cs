using System;
using UnityEngine;

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

        private void OnEnable()
        {
            RespawnPlayer += SetPlayerPosition;
        }

        private void OnDisable()
        {
            RespawnPlayer -= SetPlayerPosition;
        }

        private void Start()
        {
            OnRespawnPlayer();
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