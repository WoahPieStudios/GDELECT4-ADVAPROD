using System;
using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI killCounterDisplay;
    private int totalScore;

    public static event Action<int> AddScore;

    private void OnEnable()
    {
        AddScore += UpdateScore;
    }

    private void Awake()
    {
        killCounterDisplay.text = $"Kill Counter: {totalScore:00}";
    }

    private void UpdateScore(int score)
    {
        totalScore += score;
        killCounterDisplay.text = $"Kill Counter: {totalScore:00}";
    }

    public static void OnAddScore(int score)
    {
        AddScore?.Invoke(score);
    }
}
