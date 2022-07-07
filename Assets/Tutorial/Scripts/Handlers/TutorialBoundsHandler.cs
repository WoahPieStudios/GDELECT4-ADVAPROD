using System;
using Tutorial.Scripts.Managers;
using UnityEngine;

namespace Tutorial.Scripts.Handlers
{
    public class TutorialBoundsHandler : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
                TutorialPlayerSpawnManager.Instance.RespawnPlayer();
        }
    }
}
