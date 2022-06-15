using Spawning.Scripts.Managers;
using UnityEngine;

namespace AdditiveScenes.Scripts.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Channels/Gameplay/New Player Respawn Channel")]
    public class PlayerRespawnChannel : ScriptableObject
    {
        public void OnRespawnPlayer()
        {
            Debug.Log("Respawn player invoked", this);
            PlayerSpawnManager.OnRespawnPlayer();
        }
    }
}