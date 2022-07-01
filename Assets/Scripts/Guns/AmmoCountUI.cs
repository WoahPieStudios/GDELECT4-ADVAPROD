using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class AmmoCountUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _maxAmmo;

    [SerializeField]
    private TextMeshProUGUI _currentAmmo;

    public static event Action<int> onUpdateAmmoCount;
    public static event Action<int> onUpdateMaxAmmoCount;

    private void OnEnable()
    {
        onUpdateAmmoCount += UpdateAmmo;
        onUpdateMaxAmmoCount += UpdateMaxAmmo;
    }

    private void OnDisable()
    {
        onUpdateAmmoCount -= UpdateAmmo;
        onUpdateMaxAmmoCount -= UpdateMaxAmmo;
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
