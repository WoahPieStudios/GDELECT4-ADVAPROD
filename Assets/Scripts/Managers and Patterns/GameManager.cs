using System;
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

    [Header("Game Events")]
    [SerializeField] private UnityEvent gameStart;
    [SerializeField] private UnityEvent gameOver;
    public bool IsGameOver { get; private set; }

    private void OnEnable()
    {
        OnGameStart();
    }

    public void OnGameStart()
    {
        IsGameOver = false;
        Time.timeScale = 1f;
        gameStart?.Invoke();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    public void OnGameOver()
    {
        IsGameOver = true;
        Time.timeScale = 0f;
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
