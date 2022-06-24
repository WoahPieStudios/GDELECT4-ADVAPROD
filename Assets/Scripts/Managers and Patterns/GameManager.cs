using System;
using AdditiveScenes.Scripts.ScriptableObjects;
using Spawning.Scripts.Managers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [Header("Game Properties")]
    [SerializeField] private int totemsToKill;
    [SerializeField] private int dronesToKill;
    [SerializeField] private int tanksToKill;
    
    [SerializeField] private float roundDuration;
    public TimeSpan roundTime => TimeSpan.FromSeconds(roundDuration);
    
    public int TotemsToKill => totemsToKill;
    public int DronesToKill => dronesToKill;
    public int TanksToKill => tanksToKill;

    [Header("System Reference")]
    [SerializeField] private PauseEventChannel pauseEventChannel;

    [Header("Game Events")]
    public UnityEvent gameStart;
    public UnityEvent gameOver;
    public UnityEvent gamePause;
    public UnityEvent gameResume;
    public bool IsPaused { get; private set; }
    public bool IsGameOver { get; private set; }

    private void OnEnable()
    {
        InputManager.onPause += OnGamePause;
        SceneManager.sceneLoaded += (arg0, mode) => { OnGameStart(); };
    }

    private void OnDisable()
    {
        InputManager.onPause -= OnGamePause;
    }

    private void Update()
    {
        if (!IsPaused && !IsGameOver)
            roundDuration += Time.deltaTime;
    }

    public void OnGamePause()
    {
        IsPaused = true;
        Cursor.visible = true;
        pauseEventChannel.OnPause();
        gamePause?.Invoke();
    }

    public void OnGameResume()
    {
        IsPaused = false;
        Cursor.visible = false;
        pauseEventChannel.OnResume();
        gameResume?.Invoke();
    }

    public void OnGameStart()
    {
        IsGameOver = false;
        //Time.timeScale = 1f;
        roundDuration = 0f;
        pauseEventChannel.SetUseUI(true);
        pauseEventChannel.OnResume();
        gameStart?.Invoke();
        print("gameStart invoked");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnGameOver()
    {
        IsGameOver = true;
        //Time.timeScale = 0f;
        pauseEventChannel.SetUseUI(false);
        pauseEventChannel.OnPause();
        gameOver?.Invoke();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void RetryGame()
    {
        OnGameStart();
        //PlayerSpawnManager.OnRespawnPlayer();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
