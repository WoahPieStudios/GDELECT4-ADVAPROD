using System.Collections.Generic;
using Spawning.Scripts.Enemies;
using UnityEngine;

namespace Spawning.Scripts.Pools
{
    public class DronePool : Singleton<DronePool>
    {
        [Header("Pool Settings")]
        [SerializeField] private Drone dronePrefab;
        [SerializeField] private Transform droneParent;
        [SerializeField] private int initialAmount;
        [SerializeField] private int maxAmount;
        public Stack<Drone> AvailableDrones { get; private set; }
        private List<Drone> _totalDrones;

        private void Reset()
        {
            droneParent = transform;
            initialAmount = 100;
            maxAmount = 300;
        }

        protected override void Awake()
        {
            base.Awake();
            InitializePool();
        }

        private void InitializePool()
        {
            AvailableDrones = new Stack<Drone>();
            _totalDrones = new List<Drone>();
            CreateDrones();
        }

        private void CreateDrones()
        {
            Debug.LogWarning(_totalDrones.Count);
            if(_totalDrones.Count <= maxAmount-initialAmount)
            {
                for (int i = 0; i < initialAmount; i++)
                {
                    var drone = Instantiate(dronePrefab, droneParent);
                    drone.gameObject.SetActive(false);
                    AvailableDrones.Push(drone);
                    _totalDrones.Add(drone);
                }
            }
            else
            {
                Debug.LogError("Max amount of drones for this scene reached.");
            }
        }

        public Drone GetDrone()
        {
            if (!AvailableDrones.TryPop(out var drone))
            {
                CreateDrones();
                return AvailableDrones.TryPop(out drone) ? drone : null;
            }
            else
            {
                drone.gameObject.SetActive(true);
                return drone;
            }
        }

        public void Release(Drone drone)
        {
            drone.gameObject.SetActive(false);
            AvailableDrones.Push(drone);
        }

    }
}