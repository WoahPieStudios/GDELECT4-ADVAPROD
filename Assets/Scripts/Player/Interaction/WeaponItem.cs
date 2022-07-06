using UnityEngine;
using System;

public class WeaponItem : MonoBehaviour
{
    public Gun gun;

    public static event Action<Gun> onGetWeapon;

    //private void OnEnable()
    //{
    //    InputManager.onPlayerInteraction += GetGun;
    //}

    //private void OnDisable()
    //{
    //    InputManager.onPlayerInteraction -= GetGun;
    //}

    private void GetGun()
    {
        onGetWeapon?.Invoke(gun);
        this.gameObject.SetActive(false);
    }
}
