using System;
using AdditiveScenes.Scripts.ScriptableObjects;
using UnityEngine;

namespace Tutorial.Scripts.Managers
{
    public class TutorialPlayerSpawnManager : Singleton<TutorialPlayerSpawnManager>
    {
        [SerializeField] private Transform startingPoint;
        [SerializeField] private SFXChannel respawnSFX;
        private Transform _playerTransform;
        private Vector3 _respawnPosition;

        private void Awake() => _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        public void SetPlayerToStartingPoint() => _playerTransform.position = startingPoint.position;
        public void RespawnPlayer()
        {
            respawnSFX.PlayAudio();
            _playerTransform.position = _respawnPosition;
        }
        public void SetRespawnPosition(Transform respawnPoint) => _respawnPosition = respawnPoint.position;
    }
}
