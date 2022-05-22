using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemySpawn.Scripts.Testing
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private bool _isMoving;
        [SerializeField, Min(1f)] private float _movementRange;

        private void Reset()
        {
            _movementRange = 1f;
        }

        private void Update()
        {
            if (!_isMoving) return;

            var _newPosition = transform.position;
            _newPosition.x += Mathf.Cos(Time.time) * Time.deltaTime * _movementRange;
            transform.position = _newPosition;
        }
    }
}
