using System;
using System.Collections.Generic;
using Enums;
using Spawning.Scripts.Enemies;
using UnityEngine;

namespace Spawning.Scripts.Pools
{
    public class DronePool : Singleton<DronePool>
    {
        [Header("Pool Settings")]
        [SerializeField] private Transform droneParent;
        [SerializeField] private int maxAmount;
        
        [Header("Lite Drone")]
        [SerializeField] private Drone dronePrefab;
        [SerializeField] private int initialAmount;
        public Stack<Drone> AvailableDrones { get; private set; }
        
        [Header("Heavy Drone")]
        [SerializeField] private Drone tankDronePrefab;
        [SerializeField] private int tankInitialAmount;
        public Stack<Drone> AvailableTankDrones { get; private set; }
        
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
            AvailableTankDrones = new Stack<Drone>();
            _totalDrones = new List<Drone>();
            CreateDrones(EnemyType.Drone);
            CreateDrones(EnemyType.Tank);
        }

        private void CreateDrones(EnemyType type)
        {
            Debug.LogWarning(_totalDrones.Count);
            // Initial amount is subtracted to check if we can still spawn for another set of the initial amount

            int _initialAmount = 0;
            switch (type)
            {
                case EnemyType.Drone:
                    _initialAmount = initialAmount;
                    break;
                case EnemyType.Tank:
                    _initialAmount = tankInitialAmount;
                    break;
            }
            
            if (_totalDrones.Count <= maxAmount - _initialAmount)
            {
                for (int i = 0; i < _initialAmount; i++)
                {
                    var drone = Instantiate(type == EnemyType.Drone ? dronePrefab : tankDronePrefab, droneParent);
                    drone.gameObject.SetActive(false);
                    switch (type)
                    {
                        case EnemyType.Drone:
                            AvailableDrones.Push(drone);
                            break;
                        case EnemyType.Tank:
                            AvailableTankDrones.Push(drone);
                            break;
                    }
                    _totalDrones.Add(drone);
                }
            }
            else
            {
                Debug.LogError("Max amount of drones for this scene reached.");
            }
        }

        public Drone GetDrone(EnemyType type)
        {
            switch (type)
            {
                case EnemyType.Drone:
                    return GetDrone();
                case EnemyType.Tank:
                    return GetHeavyDrone();
                default:
                    return null;
            }
        }
        
        private Drone GetDrone()
        {
            if (!AvailableDrones.TryPop(out var drone))
            {
                CreateDrones(EnemyType.Drone);

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
        
        private Drone GetHeavyDrone()
        {
            if (!AvailableTankDrones.TryPop(out var drone))
            {
                CreateDrones(EnemyType.Tank);

                if (AvailableTankDrones.TryPop(out drone))
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
            if(!drone.isInitialized) return;
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