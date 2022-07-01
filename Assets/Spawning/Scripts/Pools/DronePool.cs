using System;
using System.Collections.Generic;
using Enums;
using Handlers;
using Spawning.Scripts.Enemies;
using UnityEngine;

namespace Spawning.Scripts.Pools
{
    public class DronePool : Singleton<DronePool>
    {
        [Header("Pool Settings")]
        [SerializeField] private int maxAmount;
        
        [Header("Lite Drone")]
        [SerializeField] private Drone dronePrefab;
        [SerializeField] private int initialAmount;
        [SerializeField] private Transform droneParent;

        public Stack<Drone> AvailableDrones { get; private set; }
        
        [Header("Heavy Drone")]
        [SerializeField] private Drone tankDronePrefab;
        [SerializeField] private int tankInitialAmount;
        [SerializeField] private Transform tankParent;
        public Stack<Drone> AvailableTankDrones { get; private set; }
        
        private List<Drone> _totalDrones;

        [Header("Hit Effect")]
        [SerializeField] private VFXHandler hitEffect;
        [SerializeField] private int initialAmountVFX;
        [SerializeField] private Transform vfxParent;

        public Stack<VFXHandler> EffectPool { get; private set; }

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

            EffectPool = new Stack<VFXHandler>();
            InitializeEffectPool();
        }

        private void InitializeEffectPool()
        {
            for (int i = 0; i < initialAmountVFX; i++)
            {
                var vfx = Instantiate(hitEffect, vfxParent);
                vfx.gameObject.SetActive(false);
                EffectPool.Push(vfx);
            }
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
                    var drone = Instantiate(
                        type == EnemyType.Drone ? dronePrefab : tankDronePrefab,
                        type == EnemyType.Drone ? droneParent : tankParent
                        );
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
            switch (drone.EnemyType)
            {
                case EnemyType.Drone:
                    AvailableDrones.Push(drone);
                    break;
                case EnemyType.Tank:
                    AvailableTankDrones.Push(drone);
                    break;
            }
        }

        public VFXHandler GetVFXHandler(Vector3 position)
        {
            if (EffectPool.TryPop(out var effect))
            {
                effect.transform.position = position;
                effect.gameObject.SetActive(true);
                return effect;
            }
            else
            {
                return null;
            }
        }
        
        public void Release(VFXHandler vfxHandler)
        {
            vfxHandler.transform.position = Vector3.zero;
            vfxHandler.gameObject.SetActive(false);
            EffectPool.Push(vfxHandler);
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