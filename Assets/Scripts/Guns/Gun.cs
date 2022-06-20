using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AdditiveScenes.Scripts.ScriptableObjects;
using System.Threading.Tasks;

public enum FireMode {
    Semi,
    Automatic
}

 
public class Gun : MonoBehaviour
{
    private bool _enableCrosshair;
    [SerializeField]
    protected SFXChannel gunSoundChannel;

    #region WEAPON STATS
    [Space]
    [Header("WEAPON STATS")]
    [SerializeField]
    protected FireMode fireMode = FireMode.Semi;
    /// <summary>
    /// rate is in ms (1/_fireRate)
    /// </summary>
    [SerializeField, Tooltip("rate is in ms (1/_fireRate)")]
    protected float fireRate = 100f;

    [SerializeField]
    protected float maxRange = 100f;

    [SerializeField]
    protected int bulletsPerMagazine = 10;
    /// <summary>
    /// radius indicates how big the spherecast will be once raycast doesn't hit but at the same time, close to hitting something
    /// </summary>
    [SerializeField, Tooltip("radius indicates how big the spherecast will be once raycast doesn't hit but at the same time, close to hitting something")]
    private float _radius;
    [SerializeField]
    private bool _resetCount = false;
    
    private int _shotsCounter;

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

    #region RELOAD MECHANIC VARIABLES
    [Space]
    [Header("RELOAD MECHANIC")]
    [SerializeField, Range(0,3f)]
    private float _reloadSpeed = 1f;
    private static event Action onReloadTime;
    private bool _isReloading;
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

    private bool _didFire;

    private Vector3 center;

    void Start()
    {
        _camera = Camera.main;
        _shotsCounter = bulletsPerMagazine;
        canShoot = true;
    }

    private void OnEnable()
    {
        InputManager.onShoot += OnPressedTrigger;
        InputManager.onReleaseShooting += OnReleasedTrigger;
        InputManager.onManualReloading += Reloading;
    }

    private void OnDisable()
    {
        InputManager.onShoot -= OnPressedTrigger;
        InputManager.onReleaseShooting -= OnReleasedTrigger;
        InputManager.onManualReloading -= Reloading;
    }
    private void Update()
    {

        CrosshairCasting();
        if (_enableCrosshair)
        {
            EnemyCrosshair.OnUpdateEnemyCH(1);
        } else
        {
            EnemyCrosshair.OnUpdateEnemyCH(0);
        }
        if (_shotsCounter == 0 && !_isReloading)
        {
            Reloading();
        }
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
            if (fireMode == FireMode.Semi)
            {
                if (_didFire) return;
            }

            _shotsCounter--;
            gunSoundChannel?.PlayAudio();
            nextShot = Time.time + 1 / fireRate;


            RaycastHit hit;
            Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, maxRange);
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                _enableCrosshair = true;
                hit.collider.gameObject.GetComponent<IDamageable>().TakeDamage(_baseDamage);
            }
            else
            {
                RaycastHit sphereHit;
                bool sphereCastDidHit = Physics.SphereCast(_camera.transform.position, _radius, _camera.transform.forward, out sphereHit, maxRange);
                if (sphereCastDidHit)
                {
                    center = sphereHit.point;
                    if (sphereHit.collider.gameObject.CompareTag("Enemy"))
                    {
                        sphereHit.collider.gameObject.GetComponent<IDamageable>().TakeDamage(_baseDamage);
                        _enableCrosshair = true;
                    }
                }
                else
                {
                    _enableCrosshair = false;
                }
            }

        }
        _didFire = true;

    }
    private void CrosshairCasting()
    {
        RaycastHit hit;
        Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, maxRange);
        if (hit.collider == null)
        {
                _enableCrosshair = false;
        }else
        {

            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                _enableCrosshair = true;
            }
            else
            {
                RaycastHit sphereHit;
                bool sphereCastDidHit = Physics.SphereCast(_camera.transform.position, _radius, _camera.transform.forward, out sphereHit, maxRange);
                if (sphereCastDidHit)
                {
                    center = sphereHit.point;
                    if (sphereHit.collider.gameObject.CompareTag("Enemy"))
                    {
                        _enableCrosshair = true;
                    }else
                    {
                        _enableCrosshair = false;
                    } 
                }
            
            }
        }

    }

    private async Task CountDown(float duration)
    {
        var currentTimer = Time.time + duration;

        while (Time.time < currentTimer)
        {
            await Task.Yield();
        }
        onReloadTime?.Invoke();
    }

    private async void Reloading()
    {
        canShoot = false;
        _isReloading = true;
        onReloadTime += Reload;
        await CountDown(_reloadSpeed);
        onReloadTime -= Reload;
        return;
    }

    private void Reload()
    {
        _isReloading = false;
        _shotsCounter = bulletsPerMagazine;
        canShoot = true;
    }

    private void OnPressedTrigger()
    {
        _triggerBeingPressed = true;
    }

    private void OnReleasedTrigger()
    {
        _triggerBeingPressed = false;
        _didFire = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(center, _radius);
    }
}
