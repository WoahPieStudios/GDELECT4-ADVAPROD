using System;
using System.Collections;
using System.Collections.Generic;
using AdditiveScenes.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private PauseEventChannel pauseEventChannel;
    private bool isPaused = false;
    private bool canPause;

    public GameObject pauseMenuUI;
    public GameObject controlUI;


    // private void OnEnable()
    // {
    //     InputManager.onPause += Pause;
    //     //InputManager.onResume += Resume;
    // }
    //
    // private void OnDisable()
    // {
    //     InputManager.onPause -= Pause;
    //   // InputManager.onResume -= Resume;
    // }

    // Update is called once per frame
    // void Update()
    // {
    //    /* if (Input.GetKeyDown(KeyCode.Escape))
    //     {
    //         if (isPaused)
    //         {
    //             Resume();
    //         } else
    //         {
    //             Pause();
    //         }
    //     }*/
    // }

    private void OnEnable()
    {
        pauseEventChannel.AddPauseListener(() => { canPause = false; });
        pauseEventChannel.AddResumeListener(() => { canPause = true; });
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        pauseEventChannel.OnResume();
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Pause()
    {
        //if (myCoroutine.isRunning) return;
        if (!canPause && isPaused) return;
        pauseMenuUI.SetActive(true);
        controlUI.SetActive(true);
        pauseEventChannel.OnPause();
        isPaused = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void LoadMenu ()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
