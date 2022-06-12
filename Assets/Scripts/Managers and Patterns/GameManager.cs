using System;
using AdditiveScenes.Scripts.ScriptableObjects;
using Spawning.Scripts.Managers;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    [Header("Game Properties")]
    [SerializeField] private int totemsToKill;
    [SerializeField] private int dronesToKill;
    [SerializeField] private int tanksToKill;
    
    public int TotemsToKill => totemsToKill;
    public int DronesToKill => dronesToKill;
    public int TanksToKill => tanksToKill;

    [Header("System Reference")]
    [SerializeField] private PauseEventChannel pauseEventChannel;
    
    [Header("Game Events")]
    [SerializeField] private UnityEvent gameStart;
    [SerializeField] private UnityEvent gameOver;
    [SerializeField] private UnityEvent gamePause;
    [SerializeField] private UnityEvent gameResume;
    public bool IsPaused { get; private set; }
    public bool IsGameOver { get; private set; }

    private void OnEnable()
    {
        InputManager.onPause += OnGamePause;
    }

    private void OnDisable()
    {
        InputManager.onPause -= OnGamePause;
    }

    public void OnGamePause()
    {
        IsPaused = true;
        pauseEventChannel.OnPause();
        gamePause?.Invoke();
    }

    public void OnGameResume()
    {
        IsPaused = false;
        pauseEventChannel.OnResume();
        gameResume?.Invoke();
    }
    
    private void Start()
    {
        OnGameStart();
    }

    public void OnGameStart()
    {
        IsGameOver = false;
        //Time.timeScale = 1f;
        pauseEventChannel.OnResume();
        gameStart?.Invoke();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    public void OnGameOver()
    {
        IsGameOver = true;
        //Time.timeScale = 0f;
        pauseEventChannel.OnPause();
        gameOver?.Invoke();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void RetryGame()
    {
        FindObjectOfType<ScoreManager>().ClearScore();
        OnGameStart();
        PlayerSpawnManager.OnRespawnPlayer();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
