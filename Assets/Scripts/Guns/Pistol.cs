using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemySpawn.Scripts.Enemies;

/// <summary>
/// - This is temporary hit scan pistol. 
/// - Only going to code for prototype
/// </summary>
public class Pistol : MonoBehaviour
{
    /// <summary>
    /// Range in Units
    /// </summary>
    [SerializeField]
    private float _maxRange;
    /// <summary>
    /// Measured in seconds
    /// </summary>
    [SerializeField, Range(0.1f, 10f), Tooltip("Measured in seconds")]
    private float _rateOfFire;
    /// <summary>
    /// damage of the pistol
    /// </summary>
    [SerializeField, Tooltip("Damage of the pistol")]
    private float _damage = 5;


    #region Cooldown Mechanic
    [Header("OverHeat mechanic")]
    [SerializeField]
    private float _overHeatRate = 4.0f;
    [SerializeField]
    private float _coolDownRate = 2.0f;
    [SerializeField]
    private float _maxOverHeat;


    /// <summary>
    /// Allowable Range from 0 to the set value so the player can shoot
    /// </summary>
    [SerializeField, Tooltip("Allowable Range from 0 to the set value")]
    private float _thresholdToAllowShootAfterCooldown;

    private bool _didShoot;

    /// <summary>
    /// Measures the current overheat status of the gun
    /// </summary>
    private float _currentOverHeatRate;

    public float currentOverHeatRate 
    { 
        get => _currentOverHeatRate;
        set => _currentOverHeatRate = Mathf.Clamp(value, 0, _maxOverHeat);
    }
    #endregion 

    /// <summary>
    /// Crosshair
    /// </summary>
    [Header("radius shot")]
    [SerializeField]
    private float _radius = 0.5f;

    private bool _canShoot;
    private Camera _camera;
    private float _nextShotTime;
    private RaycastHit _point;

    void Start()
    {
        _nextShotTime = 0;
        _camera = Camera.main;
        _canShoot = true;
    }

    private void OnEnable()
    {
        InputManager.onShoot += Shoot;
    }

    private void OnDisable()
    {
        InputManager.onShoot -= Shoot;
    }

    private void Update()
    {
        if (_didShoot)
        {
            _didShoot = false;
            currentOverHeatRate += _overHeatRate;
            if (currentOverHeatRate >= _maxOverHeat)
            {
                _canShoot = false;
            }
        }
        else
        {

            currentOverHeatRate -= Time.deltaTime * _coolDownRate;
            if (currentOverHeatRate <= _thresholdToAllowShootAfterCooldown)
            {
                _canShoot = true;
            }
        }

    }

    private void Shoot()
    {
        if (!_canShoot) return;


        if (Time.time > _nextShotTime)
        {

            _nextShotTime = Time.time + _rateOfFire;

            //add overheat mechanic here
            _didShoot = true;

            RaycastHit hit;
            if (Physics.SphereCast(_camera.transform.position, _radius, _camera.transform.forward,out hit, _maxRange))
            {
                _point = hit;
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    Debug.Log("Enemy hit!");
                    hit.collider.gameObject.GetComponent<Drone>().TakeDamage(_damage);
                }else
                {
                    Debug.Log("Did hit something");
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(_point.point, _radius);
    }
}
