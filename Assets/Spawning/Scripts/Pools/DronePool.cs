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
            // Initial amount is subtracted to check if we can still spawn for another set of the initial amount
            if (_totalDrones.Count <= maxAmount - initialAmount)
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

                if (AvailableDrones.TryPop(out drone))
                {
                    drone.gameObject.SetActive(true);
                    return drone;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                drone.gameObject.SetActive(true);
                return drone;
            }
        }

        public void Release(Drone drone)
        {
            if (!drone.isInitialized) return;
            drone.gameObject.SetActive(false);
            AvailableDrones.Push(drone);
        }

        public void ClearDrones()
        {
            foreach (var drone in _totalDrones)
            {
                Release(drone);
            }
        }
        
    }
}