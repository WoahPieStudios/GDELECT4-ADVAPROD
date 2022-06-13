using UnityEngine;
using System;

public class EnemyCrosshair : CrosshairChange 
{
    private static event Action<int> onCrosshairUpdateEnemy;
    void Start()
    {
        // Always make the first iteration the default
        crosshairImage.sprite = crosshairSprites[0];
    }

    private void OnEnable()
    {
        onCrosshairUpdateEnemy += UpdateCrosshair;
    }

    private void OnDisable()
    {
        onCrosshairUpdateEnemy -= UpdateCrosshair;
    }

    protected override void UpdateCrosshair(int index)
    {
        if (index == 0)
        {
            crosshairImage.sprite = crosshairSprites[0];
        }
        else if (index == 1)
        {
            crosshairImage.sprite = crosshairSprites[1];
        }
        else
        {
            Debug.LogError("No more Index");
        }
    }

    public static void OnUpdateEnemyCH(int index) => onCrosshairUpdateEnemy?.Invoke(index);
}
