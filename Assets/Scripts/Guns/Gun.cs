using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum FireMode {
    Semi,
    Automatic
}

 
public class Gun : MonoBehaviour
{

    [SerializeField]
    protected AudioClip gunSound;

    #region WEAPON STATS
    [Space]
    [Header("WEAPON STATS")]
    [SerializeField]
    protected FireMode fireMode = FireMode.Semi;
    /// <summary>
    /// rate is in ms (_fireRate/1000)
    /// </summary>
    [SerializeField, Tooltip("rate is in ms (_fireRate/1000)")]
    protected float fireRate = 100f;

    [SerializeField]
    protected float maxRange = 100f;

    /// <summary>
    /// radius indicates how big the spherecast will be once raycast doesn't hit but at the same time, close to hitting something
    /// </summary>
    [SerializeField, Tooltip("radius indicates how big the spherecast will be once raycast doesn't hit but at the same time, close to hitting something")]
    private float _radius;


    #region DAMAGE 
    [Space]
    [Header("WEAPON DAMAGE")]
    /// <summary>
    /// base damage for the weapon
    /// </summary>
    [SerializeField, Tooltip("Base Damage for the weapon")]
    private float _baseDamage;
    /// <summary>
    /// multiplier for headshot
    /// </summary>
    [SerializeField, Tooltip("multiplier for headshot")]
    private float _headshotDamageMultiplier;
    #endregion

    #endregion

    #region OVERHEAT MECHANIC VARIABLES
    [Space]
    [Header("OVERHEAT MECHANIC")]
    [SerializeField]
    private float _overHeatMultiplier = 1f;
    [SerializeField]
    private float _coolDownMultiplier = 1f;
    [SerializeField]
    private float _maxCapacity = 10f;
    /// <summary>
    /// minimum thermals before player can shoot
    /// </summary>
    [SerializeField, Tooltip("minimum thermals before player can shoot")]
    private float _thresholdBeforeAllowingToShoot = 1;

    /// <summary>
    /// this is for the 
    /// </summary>
    private float _currentTemp;
    #endregion


    /// <summary>
    /// for purposes when the player cannot shoot due to overheat or other third party effects
    /// </summary>
    protected bool canShoot;
    /// <summary>
    /// Indicates when the next bullet can be shot
    /// </summary>
    protected float nextShot;
    private Camera _camera;
    /// <summary>
    /// for indication when the trigger is being pressed
    /// </summary>
    private bool _triggerBeingPressed;

    private Vector3 center;

    void Start()
    {
        _camera = Camera.main;

        canShoot = true;
    }

    private void OnEnable()
    {
        InputManager.onShoot += OnPressedTrigger;
        InputManager.onReleaseShooting += OnReleasedTrigger;
    }

    private void OnDisable()
    {
        InputManager.onShoot -= OnPressedTrigger;
        InputManager.onReleaseShooting -= OnReleasedTrigger;
    }
    private void Update()
    {
        OverHeat();
    }

    private void FixedUpdate()
    {
        if (!_triggerBeingPressed) return;

        Debug.DrawRay(_camera.transform.position, _camera.transform.forward * maxRange, Color.red);
        Shoot();
        
    }

    /// <summary>
    /// can be altered via use of projectile type weapons
    /// </summary>
    protected virtual void Shoot()
    {
        if (!canShoot) return;

        if(Time.time > nextShot)
        {
            SoundManager.Instance.OnPlaySFX(gunSound);
            nextShot = Time.time + 1 / fireRate;
            
            if (fireMode == FireMode.Semi)
            {
                if (_triggerBeingPressed) return;
            }

            RaycastHit hit;
            Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, maxRange);
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Using Raycast");

                Debug.Log("Enemy Raycast Hit");
                hit.collider.gameObject.GetComponent<IDamageable>().TakeDamage(_baseDamage);
                
            }
            else
            {
                RaycastHit sphereHit;
                bool sphereCastDidHit = Physics.SphereCast(_camera.transform.position, _radius, _camera.transform.forward, out sphereHit, maxRange);
                if (sphereCastDidHit)
                {
                    Debug.Log("Using SphereCast");
                    center = sphereHit.point;
  
                    if (sphereHit.collider.gameObject.CompareTag("Enemy"))
                    {
                        Debug.Log("Enemy Spherecast Hit");
                        sphereHit.collider.gameObject.GetComponent<IDamageable>().TakeDamage(_baseDamage);
                    }

                }

            }
          
        }
    }

    private void OverHeat()
    {
        _currentTemp = Mathf.Clamp(_currentTemp, 0, _maxCapacity);
        if (!_triggerBeingPressed || !canShoot)
        {
            _currentTemp -= Time.deltaTime * _coolDownMultiplier;
            if (_currentTemp <= _thresholdBeforeAllowingToShoot)
            {
                canShoot = true;
            }
        }
        if(_triggerBeingPressed && canShoot)
        {

            _currentTemp += Time.deltaTime * _overHeatMultiplier;
            if (_currentTemp >= _maxCapacity)
            {
                canShoot = false;
            }
        }

    }

    private void OnPressedTrigger()
    {
        _triggerBeingPressed = true;
    }

    private void OnReleasedTrigger()
    {
        _triggerBeingPressed = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(center, _radius);
    }
}
