using System;
using System.Collections;
using System.Collections.Generic;
using AdditiveScenes.Scripts.ScriptableObjects;
using Handlers;
using Spawning.Scripts.Spawners;
using UnityEngine;

public class Weakpoint : MonoBehaviour, IDamageable
{
    [SerializeField] private float health;
    [SerializeField] private DroneSpawner droneSpawner;
    [SerializeField] private VFXHandler explosionVFX;
    [SerializeField] private SFXChannel explosionSFX;
    private Material _material;
    private float maxHealth;

    private void Start()
    {
        maxHealth = health;
        _material = GetComponent<Renderer>().material;
    }

    public float Health
    {
        get => health;
        set
        {
            if (value <= 0)
                health = 0;
            else if (value >= maxHealth)
                health = maxHealth;
            else
                health = value;
            
        }
    }

    public void TakeDamage(float damageAmount)
    {
        Health -= damageAmount;
        var color = Color.Lerp(Color.black, Color.white, Health / maxHealth);
        _material.color = color;
        if (Health <= 0)
            GetDestroyed();
    }
    
    public void GetDestroyed(bool killedByPlayer = true)
    {
        droneSpawner.TakeDamage(Health);
        if (killedByPlayer)
        {
            explosionSFX.PlayAudio();
            Instantiate(explosionVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
