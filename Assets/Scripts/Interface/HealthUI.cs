using System;
using Spawning.Scripts.Combat;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI healthDisplay;

    private void OnEnable()
    {
        PlayerCombat.onHealthUpdate += UpdateHealthUI;
    }
    private void OnDisable()
    {
        PlayerCombat.onHealthUpdate += UpdateHealthUI;
    }

    private void UpdateHealthUI()
    {
        healthDisplay.text = $"{FindObjectOfType<PlayerCombat>().Health}";
        fillImage.fillAmount = FindObjectOfType<PlayerCombat>().Health / 100;
    }
}
