using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class AmmoCountUI : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI _maxAmmo;

    [SerializeField]
    private TextMeshProUGUI _currentAmmo;
    private void OnEnable()
    {
        Gun.onUpdateCurrentAmmoUI += UpdateAmmo;
        Gun.onUpdateMaxAmmoUI += UpdateMaxAmmo;
    }

    private void OnDisable()
    {
        Gun.onUpdateCurrentAmmoUI -= UpdateAmmo;
        Gun.onUpdateMaxAmmoUI -= UpdateMaxAmmo;
    }


    private void UpdateAmmo(int ammo)
    {
        _currentAmmo.text = ammo.ToString();
    }

    private void UpdateMaxAmmo(int maxAmmo)
    {
        _maxAmmo.text = maxAmmo.ToString();
    }

}
