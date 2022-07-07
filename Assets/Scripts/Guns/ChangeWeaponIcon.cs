using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChangeWeaponIcon : MonoBehaviour
{

    private Image _iconContainer;

    private void Awake()
    {
        _iconContainer = GetComponent<Image>();
    }

    private void OnEnable()
    {
        WeaponItem.onGetIcon += ChangeIcon;
    }

    private void OnDisable()
    {
        WeaponItem.onGetIcon -= ChangeIcon;
    }

    private void ChangeIcon(Sprite changeIcon)
    {
        _iconContainer.sprite = changeIcon;
    }




}
