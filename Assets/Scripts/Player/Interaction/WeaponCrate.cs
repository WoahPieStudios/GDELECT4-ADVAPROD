using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;



public class WeaponCrate : MonoBehaviour
{
    [SerializeField, Tooltip("How many seconds to fully interact")]
    private float _holdInteraction = 3f;
    [SerializeField]
    private Image _filler;
    [SerializeField, Tooltip("Spawn location of the weapon")]
    private Transform _spawnLocation;
    [SerializeField]
    private GameObject _UI;

    private bool _canInteract;
    private float amt = 0;
    [Header("Weapon List")]
    [SerializeField]
    private List<GunItem> _weaponsList = new List<GunItem>();

    

    void Start()
    {
        _filler.fillAmount = 0; 
    }


    private void OnEnable()
    {
        InputManager.onPlayerCancelInteraction += CancelInteraction;
        _canInteract = true;
        _UI.SetActive(true);
    }

    private void OnDisable()
    {
        InputManager.onPlayerCancelInteraction -= CancelInteraction;
    }


    private WeaponItem GetRandomWeapon()
    {
        float totalWeight = 0;

        foreach(GunItem weapon in _weaponsList)
        {
            totalWeight += weapon.weight;   
        }

        System.Random r = new System.Random();
        double randomWeight = r.NextDouble() * totalWeight;

        foreach (GunItem weapon in _weaponsList) { 
            randomWeight -= weapon.weight;

            if (randomWeight > 0) { continue; }

            return weapon.item;
        }

        return null;
    }

    public void Holding(float duration)
    {
        if (!_canInteract) return;
        
        amt += duration / _holdInteraction;
        _filler.fillAmount = amt ;
        Debug.Log($"{amt}");
        if (amt >= 1)
        {
            WeaponItem weapon = Instantiate(GetRandomWeapon(), _spawnLocation);
            weapon.transform.position = _spawnLocation.position; 
            weapon.transform.rotation = _spawnLocation.rotation; 
            weapon.gameObject.SetActive(true);

            _canInteract = false;
            _UI.SetActive(false);
        }
    }

    private void CancelInteraction()
    {
        _filler.fillAmount = 0;
        amt = 0;
    }
}

[System.Serializable]
public class GunItem {
    public WeaponItem item;
    [Range(0.1f, 100f)]
    public float weight;

}