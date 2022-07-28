using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class ChangeWeaponIcon : MonoBehaviour
{
    public static event Func<Sprite> onChangeIconSprite;

    public static event Action<Sprite> onIconChange;

    private Image _iconContainer;


    public Image iconContainer
    {
        get {

            _iconContainer.sprite = onChangeIconSprite?.Invoke();
            return _iconContainer;
        }

        set => _iconContainer = value;
    }

    private void Awake()
    {
        _iconContainer = GetComponent<Image>();
    }

    private void OnEnable()
    {
        onIconChange += ChangeIcon;
    }

    private void OnDisable()
    {
        onIconChange -= ChangeIcon;
    }

    private void ChangeIcon(Sprite changeIcon)
    {
        _iconContainer.sprite = changeIcon;
    }


    public static void OnChangeIcon(Sprite icon) { onIconChange?.Invoke(icon); }

}
