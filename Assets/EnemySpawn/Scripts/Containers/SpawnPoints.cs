using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EnemySpawn.Scripts.Containers
{
    public class SpawnPoints : MonoBehaviour
    {
        [SerializeField] private List<Transform> points;

        private void Reset()
        {
            points = GetComponentsInChildren<Transform>().Where(t => t != transform).ToList();
        }
    }
}
