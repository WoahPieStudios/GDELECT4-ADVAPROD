using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AdditiveScenes.Scripts.ScriptableObjects;
using System.Threading.Tasks;
using DG.Tweening;
using Handlers;

public enum FireMode {
    Semi,
    Automatic
}

 
public class Gun : MonoBehaviour
{
    private bool _enableCrosshair;
    
    #region EFFECTS
    [Header("Effects")]
    [SerializeField]
    protected SFXChannel gunSoundChannel;
    
    [SerializeField]
    private VFXHandler droneHitEffect;

    [Header("MuzzleFlash")]
    [SerializeField] private Transform muzzlePoint;
    [SerializeField] private VFXHandler muzzleFlash;
    
    [Header("Animations")]
    [SerializeField] private Animator animator;
    #endregion
    
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
    private float _maxRange = 100f;

    [SerializeField]
    private int _bulletsPerMagazine = 10;
    /// <summary>
    /// radius indicates how big the spherecast will be once raycast doesn't hit but at the same time, close to hitting something
    /// </summary>
    [SerializeField, Tooltip("radius indicates how big the spherecast will be once raycast doesn't hit but at the same time, close to hitting something")]
    private float _radius;
    [SerializeField]
    private bool _resetCount = false;
    
    private int _shotsCounter;

    public static event Action<int> onUpdateCurrentAmmoUI;
    public static event Action<int> onUpdateMaxAmmoUI;

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
    [Range(0,3f)]
    public float _reloadSpeed = 1f;
    protected static event Action onReloadTime;
    protected bool _isReloading;
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
        _shotsCounter = _bulletsPerMagazine;
        onUpdateCurrentAmmoUI?.Invoke(_shotsCounter);
        canShoot = true;
    }

    private void OnEnable()
    {
        onUpdateMaxAmmoUI?.Invoke(_bulletsPerMagazine);
        onUpdateCurrentAmmoUI?.Invoke(_shotsCounter);
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

        Debug.DrawRay(_camera.transform.position, _camera.transform.forward * _maxRange, Color.red);
        Shoot();

    }

    /// <summary>
    /// can be altered via use of projectile type weapons
    /// </summary>
    public virtual void Shoot()
    {
        if (!canShoot) return;

        if(Time.time > nextShot)
        {
            if (fireMode == FireMode.Semi)
            {
                if (_didFire) return;
            }

            _shotsCounter--;
            onUpdateCurrentAmmoUI?.Invoke(_shotsCounter);
            animator.SetTrigger("isShooting");
            gunSoundChannel?.PlayAudio();
            Instantiate(muzzleFlash, muzzlePoint);
            nextShot = Time.time + 1 / fireRate;


            RaycastHit hit;
            Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, _maxRange);
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                _enableCrosshair = true;
                hit.collider.gameObject.GetComponent<IDamageable>().TakeDamage(_baseDamage);
            }
            else
            {
                RaycastHit sphereHit;
                bool sphereCastDidHit = Physics.SphereCast(_camera.transform.position, _radius, _camera.transform.forward, out sphereHit, _maxRange);
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

            if(hit.collider != null){
                Instantiate(droneHitEffect, hit.point, Quaternion.identity);
            }
        }
        _didFire = true;

    }
    private void CrosshairCasting()
    {
        RaycastHit hit;
        Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, _maxRange);
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
                bool sphereCastDidHit = Physics.SphereCast(_camera.transform.position, _radius, _camera.transform.forward, out sphereHit, _maxRange);
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
        if (_shotsCounter == _bulletsPerMagazine) return;
        canShoot = false;
        _isReloading = true;
        animator.SetBool("isReloading", _isReloading);
        
        onReloadTime += Reload;
        await CountDown(_reloadSpeed);
        onReloadTime -= Reload;
        return;
    }

    private void Reload()
    {
        _isReloading = false;
        animator.SetBool("isReloading", _isReloading);
        _shotsCounter = _bulletsPerMagazine;
        onUpdateCurrentAmmoUI?.Invoke(_shotsCounter);
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
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(muzzlePoint.position, Vector3.one * 0.1f);
    }
}
