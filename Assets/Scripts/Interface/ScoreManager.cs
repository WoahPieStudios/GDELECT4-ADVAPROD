using System;
using Enums;
using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Transform objectivesDisplay;
    [SerializeField] private TextMeshProUGUI objectiveDisplay;
    private TextMeshProUGUI _totemDisplay, _droneDisplay, _tankDisplay, _timeDisplay; 
    private int _totalScore, _droneScore, _tankScore, _totemScore;
    private int _roundDroneScore, _roundTankScore, _roundTotemScore;
    public static event Action<int,EnemyType> AddScore;

    [Header("Game Over Displays")]
    [SerializeField] private TextMeshProUGUI timeSurvived;
    [SerializeField] private TextMeshProUGUI enemiesKilled;
    [SerializeField] private TextMeshProUGUI towersKilled;

    private void OnEnable()
    {
        AddScore += UpdateScore;
        GameManager.Instance.gameStart.AddListener(ClearScore);
    }

    private void OnDisable()
    {
        AddScore -= UpdateScore;
        GameManager.Instance.gameStart.RemoveListener(ClearScore);
    }

    public void SetupObjectiveDisplays()
    {
        if (GameManager.Instance.TotemsToKill > 0)
        {
            _totemDisplay ??= Instantiate(objectiveDisplay, objectivesDisplay);
            _totemDisplay.text = $"Totems killed: {_totemScore:00} / {GameManager.Instance.TotemsToKill:00}";
        }

        if (GameManager.Instance.DronesToKill > 0)
        {
            _droneDisplay ??= Instantiate(objectiveDisplay, objectivesDisplay);
            _droneDisplay.text = $"Drones killed: {_droneScore:00} / {GameManager.Instance.DronesToKill:00}";
        }

        if (GameManager.Instance.TanksToKill > 0)
        {
            _tankDisplay ??= Instantiate(objectiveDisplay, objectivesDisplay);
            _tankDisplay.text = $"Tanks killed: {_tankScore:00} / {GameManager.Instance.TanksToKill:00}";
        }

        _timeDisplay ??= Instantiate(objectiveDisplay, objectivesDisplay);
    }

    private void Update()
    {
        _timeDisplay.text = $"Round Time: {GameManager.Instance.roundTime:hh\\:mm\\:ss}";
        UpdateGameOverUI();
    }

    private void UpdateGameOverUI()
    {
        timeSurvived.text = $"1. Time Survived: {GameManager.Instance.roundTime:hh\\:mm\\:ss}";
        enemiesKilled.text = $"2. Enemies killed: {_roundDroneScore:00} drones | {_roundTankScore:00} tanks";
        towersKilled.text = $"3. Towers Destroyed: {_roundTotemScore:00} Towers";
    }

    private void UpdateScore(int score, EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Totem:
                _totemScore += score;
                break;
            case EnemyType.Drone:
                _droneScore += score;
                break;
            case EnemyType.Tank:
                _tankScore += score;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
     
        _totalScore += score;
   
        UpdateScoreDisplays();
        
    }

    public static void OnAddScore(int score,EnemyType type)
    {
        if(!GameManager.Instance.IsGameOver)
            AddScore?.Invoke(score, type);
    }

    public void UpdateScoreDisplays()
    {
        if(_totemDisplay != null){
            _totemDisplay.fontStyle = _totemScore >= GameManager.Instance.TotemsToKill
                ? FontStyles.Strikethrough
                : FontStyles.Normal;
            _totemDisplay.text = $"Totems killed: {_totemScore:00} / {GameManager.Instance.TotemsToKill:00}";
        }

        if(_droneDisplay != null){
            _droneDisplay.fontStyle = _droneScore >= GameManager.Instance.DronesToKill
                ? FontStyles.Strikethrough
                : FontStyles.Normal;
            _droneDisplay.text = $"Drones killed: {_droneScore:00} / {GameManager.Instance.DronesToKill:00}";
        }

        if(_tankDisplay != null){
            _tankDisplay.fontStyle = _tankScore >= GameManager.Instance.TanksToKill
                ? FontStyles.Strikethrough
                : FontStyles.Normal;
            _tankDisplay.text = $"Tanks killed: {_tankScore:00} / {GameManager.Instance.TanksToKill:00}";
        }
    }
    
    public void ClearScore()
    {
        _roundDroneScore = _droneScore;
        _roundTankScore = _tankScore;
        _roundTotemScore = _totemScore;
        
        _totalScore = 0;
        _droneScore = 0;
        _totemScore = 0;
        _tankScore = 0;
        UpdateScoreDisplays();
    }
}
