using System;
using AdditiveScenes.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class MainMenu : MonoBehaviour
{
    public Transform startBtn;
    public Transform settingsBtn;
    public Transform quitBtn;
    public Transform GameTitle;
    public Transform aboutBtn;
    [SerializeField] private AdditiveLoadSceneChannel mainMenuChannel;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += (arg0, mode) => { mainMenuChannel.SetSceneActive(); };
    }

    private void Start()
    {
        Move();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void Move()
    {
        startBtn.DOMoveX(350, 1).SetEase(Ease.InOutSine);
        aboutBtn.DOMoveX(350, 1).SetEase(Ease.InOutSine);
        settingsBtn.DOMoveX(350, 1).SetEase(Ease.InOutSine);
        quitBtn.DOMoveX(350, 1).SetEase(Ease.InOutSine);

        GameTitle.DOMoveY(700, 1).SetEase(Ease.InOutSine);
    }
}
