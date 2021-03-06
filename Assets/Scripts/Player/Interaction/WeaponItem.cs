using UnityEngine;
using System;
using UnityEngine.UI;
using StringManager;

public class WeaponItem : MonoBehaviour
{
    public Gun gun;
    public Sprite weaponIcon;

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
        ChangeWeaponIcon.OnChangeIcon(weaponIcon);
        this.gameObject.SetActive(false);
    }
}
