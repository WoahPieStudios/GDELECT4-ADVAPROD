using System;
using System.Collections.Generic;
using UnityEngine;

namespace Flocking.Revised.Scripts
{
    [RequireComponent(typeof(Collider))]
    public class Boid : MonoBehaviour
    {
        private BoidPool _pool;
        private Transform _target;

        public void Initialize(BoidPool pool, Vector3 position, Quaternion rotation, Transform target)
        {
            _pool = pool;
            transform.position = position;
            transform.rotation = rotation;
            _target = target;
        }

        private void Update()
        {
            var direction = _target.position - transform.position;
            transform.up = direction;
            transform.position += direction * Time.deltaTime;
        }

    }
}