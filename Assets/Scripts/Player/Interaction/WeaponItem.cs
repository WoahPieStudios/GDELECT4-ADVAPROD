using UnityEngine;
using System;
using UnityEngine.UI;

public class WeaponItem : MonoBehaviour
{
    public Gun gun;

    [SerializeField]
    private Sprite _weaponIcon;

    public static event Action<Gun> onGetWeapon;
    public static event Action<Sprite> onGetIcon;

    private void OnDisable()
    {
        InputManager.onPlayerInteraction -= GetGun;
    }
     
    public void EnableInteraction()
    {
        InputManager.onPlayerInteraction += GetGun;
    }
    private void GetGun()
    {
        onGetWeapon?.Invoke(gun);
        onGetIcon?.Invoke(_weaponIcon);
        this.gameObject.SetActive(false);
    }
}
